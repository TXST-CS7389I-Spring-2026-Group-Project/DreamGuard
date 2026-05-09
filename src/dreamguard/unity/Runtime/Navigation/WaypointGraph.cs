using System.Collections.Generic;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// Per-room waypoint graph for maze navigation.
    ///
    /// Setup:
    ///   1. Add this component to each Room GameObject (alongside RoomExperiment).
    ///   2. Place <see cref="WaypointNode"/> GameObjects as children of the Room and
    ///      connect their neighbors in the Inspector.
    ///   3. Mark exactly one node per room as <c>isEnd = true</c> — place it at the
    ///      room's exit corridor. The arrow points here after all orbs are collected.
    ///   4. Assign the WaypointGraph reference in RoomExperiment's Inspector; it calls
    ///      <see cref="SetActive"/> on room enter to make this the active graph.
    ///
    /// Edges are treated as bidirectional — listing a neighbor in one direction is enough.
    /// Pathfinding uses Dijkstra weighted by world-space distance.
    /// </summary>
    public class WaypointGraph : MonoBehaviour
    {
        /// <summary>The graph for the room the player is currently in.</summary>
        public static WaypointGraph Instance { get; private set; }

        /// <summary>
        /// Mirror of OrbArrowUI.waypointAdvanceRadius for use by WaypointNode gizmos.
        /// Set by OrbArrowUI.OnValidate — lives here to avoid a circular assembly reference
        /// (DreamGuard.Player already references DreamGuard.Navigation, not vice versa).
        /// </summary>
        public static float GizmoAdvanceRadius { get; set; } = 1.0f;

        /// <summary>
        /// Activates <paramref name="graph"/> as the current room's navigation graph.
        /// Pass null to clear (arrow falls back to direct pointing).
        /// Called by RoomExperiment on room enter.
        /// </summary>
        public static void SetActive(WaypointGraph graph)
        {
            Instance = graph;
            DreamGuardLog.Log($"[WaypointGraph] SetActive — {(graph != null ? graph.name : "null")}");
        }

        /// <summary>
        /// The exit node for this room. The arrow points here after all orbs are collected.
        /// Null if no node has <c>isEnd = true</c>.
        /// </summary>
        public WaypointNode EndNode { get; private set; }

        private readonly List<WaypointNode> _nodes = new();
        private readonly Dictionary<WaypointNode, List<WaypointNode>> _adjacency = new();
        private readonly Dictionary<WaypointNode, int> _nodeIndex = new();

        // Dijkstra result cache — recomputed only when the target node changes
        // (i.e., when an orb is collected), not every frame.
        private WaypointNode _cachedEndNode;
        private Dictionary<WaypointNode, WaypointNode> _cachedNextHop; // node → next hop toward endNode

        private void Awake()
        {
            BuildGraph();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void BuildGraph()
        {
            _nodes.Clear();
            _adjacency.Clear();
            _nodeIndex.Clear();
            EndNode = null;

            // Collect only this room's nodes (children of this GameObject).
            var found = GetComponentsInChildren<WaypointNode>();
            foreach (var node in found)
            {
                _nodes.Add(node);
                if (node.IsEnd)
                {
                    if (EndNode != null)
                        DreamGuardLog.LogWarning($"[WaypointGraph] Multiple end nodes found on '{name}' — using first");
                    else
                        EndNode = node;
                }
            }

            // Build bidirectional adjacency.
            foreach (var node in _nodes)
                _adjacency[node] = new List<WaypointNode>();

            foreach (var node in _nodes)
            {
                foreach (var neighbor in node.Neighbors)
                {
                    if (neighbor == null) continue;
                    if (!_adjacency.ContainsKey(neighbor))
                    {
                        _nodes.Add(neighbor);
                        _adjacency[neighbor] = new List<WaypointNode>();
                    }
                    if (!_adjacency[node].Contains(neighbor))
                        _adjacency[node].Add(neighbor);
                    if (!_adjacency[neighbor].Contains(node))
                        _adjacency[neighbor].Add(node);
                }
            }

            // Stable index map for deterministic Dijkstra tie-breaking.
            for (int i = 0; i < _nodes.Count; i++)
                _nodeIndex[_nodes[i]] = i;

            DreamGuardLog.Log($"[WaypointGraph] Built graph on '{name}' — {_nodes.Count} nodes, " +
                              $"endNode={(EndNode != null ? EndNode.name : "none")}");
        }

        /// <summary>
        /// Returns the ordered sequence of world-space positions the player should
        /// navigate through to reach <paramref name="targetPos"/> from
        /// <paramref name="fromPos"/>, computed via Dijkstra on this room's graph.
        ///
        /// The list starts with the first waypoint the player should walk toward
        /// and ends with <paramref name="targetPos"/> (the orb or fallback target).
        /// Returns an empty list when the graph is empty or no path is found.
        ///
        /// Dijkstra is cached per target node and recomputed only when the target changes.
        /// </summary>
        public List<Vector3> GetPath(Vector3 fromPos, Vector3 targetPos)
        {
            var path = new List<Vector3>();
            if (_nodes.Count == 0) return path;

            WaypointNode endNode = FindNearestNode(targetPos);
            if (endNode == null) return path;

            // Recompute Dijkstra only when the target node changes.
            if (endNode != _cachedEndNode)
            {
                _cachedNextHop = RunDijkstraFrom(endNode);
                _cachedEndNode = endNode;
                DreamGuardLog.Log($"[WaypointGraph] Dijkstra recomputed toward '{endNode.name}' at {endNode.transform.position}");
            }

            WaypointNode startNode = FindNearestNode(fromPos);
            if (startNode == null) { path.Add(targetPos); return path; }

            if (startNode == endNode) { path.Add(targetPos); return path; }

            // Walk the precomputed next-hop chain from startNode to endNode.
            var current = startNode;
            int maxSteps = _nodes.Count;
            while (current != endNode && maxSteps-- > 0)
            {
                if (!_cachedNextHop.TryGetValue(current, out var next))
                {
                    DreamGuardLog.LogWarning($"[WaypointGraph] Path broken at '{current.name}' — graph may be disconnected");
                    break;
                }
                current = next;
                path.Add(current.transform.position);
            }

            // targetPos is the actual orb position, which may be slightly past the nearest node.
            path.Add(targetPos);
            return path;
        }

        /// <summary>
        /// Runs Dijkstra outward from <paramref name="sourceNode"/> on the undirected graph.
        /// Returns nextHop[X] = the first node to visit when traveling from X toward sourceNode.
        ///
        /// Because the graph is undirected, running from sourceNode outward is equivalent
        /// to computing all-pairs distances to sourceNode — a single O(V²) pass.
        /// </summary>
        private Dictionary<WaypointNode, WaypointNode> RunDijkstraFrom(WaypointNode sourceNode)
        {
            var dist    = new Dictionary<WaypointNode, float>(_nodes.Count);
            var nextHop = new Dictionary<WaypointNode, WaypointNode>(_nodes.Count);
            var unvisited = new List<WaypointNode>(_nodes);

            foreach (var node in _nodes)
                dist[node] = float.MaxValue;
            dist[sourceNode] = 0f;

            while (unvisited.Count > 0)
            {
                // Find the unvisited node with minimum distance; break ties by node index.
                WaypointNode current = null;
                float minDist = float.MaxValue;
                foreach (var node in unvisited)
                {
                    float d = dist[node];
                    if (d < minDist)
                    {
                        minDist = d; current = node;
                    }
                    else if (d == minDist && current != null)
                    {
                        if (_nodeIndex.GetValueOrDefault(node, int.MaxValue) <
                            _nodeIndex.GetValueOrDefault(current, int.MaxValue))
                            current = node;
                    }
                }

                if (current == null) break;
                unvisited.Remove(current);

                if (!_adjacency.TryGetValue(current, out var neighbors)) continue;
                foreach (var neighbor in neighbors)
                {
                    if (!unvisited.Contains(neighbor)) continue;
                    float alt = dist[current] + Vector3.Distance(
                        current.transform.position, neighbor.transform.position);
                    if (alt < dist[neighbor])
                    {
                        dist[neighbor] = alt;
                        nextHop[neighbor] = current;
                    }
                }
            }

            return nextHop;
        }

        /// <summary>Returns the geometrically nearest node to <paramref name="pos"/>.</summary>
        private WaypointNode FindNearestNode(Vector3 pos)
        {
            WaypointNode nearest = null;
            float nearestSqDist = float.MaxValue;
            foreach (var node in _nodes)
            {
                if (node == null) continue;
                float sqDist = (node.transform.position - pos).sqrMagnitude;
                if (sqDist < nearestSqDist) { nearestSqDist = sqDist; nearest = node; }
            }
            return nearest;
        }
    }
}
