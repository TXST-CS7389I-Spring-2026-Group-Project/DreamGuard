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

        // Hysteresis: only switch the start node when the candidate offers meaningfully
        // better total path cost (player→node + node→target). Prevents oscillation when
        // the player is near the cost-optimal crossover point between two nodes.
        private const float StartNodeSwitchThreshold = 0.5f; // world units
        private WaypointNode _lastStartNode;

        // Dijkstra result cache — recomputed only when the target node changes
        // (i.e., when an orb is collected), not every frame.
        private WaypointNode _cachedEndNode;
        private Dictionary<WaypointNode, float>       _cachedDistToEnd; // node → cost to reach endNode
        private Dictionary<WaypointNode, WaypointNode> _cachedNextHop;  // node → next hop toward endNode

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
        /// Returns the world-space position of the next waypoint node along the
        /// shortest path from <paramref name="fromPos"/> to <paramref name="targetPos"/>.
        /// Returns null if the graph is empty or no path exists.
        /// </summary>
        public Vector3? GetNextWaypointToward(Vector3 fromPos, Vector3 targetPos)
        {
            var (wp1, _) = GetNextTwoWaypointsToward(fromPos, targetPos);
            return wp1;
        }

        /// <summary>
        /// Returns the first and second waypoint positions along the shortest path from
        /// <paramref name="fromPos"/> to <paramref name="targetPos"/>.
        /// The second position falls back to <paramref name="targetPos"/> when the path
        /// is only one hop long. Both are null when no path exists.
        ///
        /// Start-node selection uses a best-cost heuristic (dist player→node + dist
        /// node→target) rather than nearest-node, so the arrow always starts by pointing
        /// in the direction that minimises total travel — even when the geometrically
        /// nearest graph node is off to the side or behind the player.
        /// Dijkstra is cached per target node and only recomputed when the target changes.
        /// </summary>
        public (Vector3? wp1, Vector3? wp2) GetNextTwoWaypointsToward(Vector3 fromPos, Vector3 targetPos)
        {
            if (_nodes.Count == 0) return (null, null);

            WaypointNode endNode = FindNearestNode(targetPos);
            if (endNode == null) return (null, null);

            // Recompute Dijkstra only when the target node changes (orb collected).
            if (endNode != _cachedEndNode)
            {
                (_cachedDistToEnd, _cachedNextHop) = RunDijkstraFrom(endNode);
                _cachedEndNode = endNode;
                DreamGuardLog.Log($"[WaypointGraph] Dijkstra recomputed toward '{endNode.name}'");
            }

            // Select the start node that minimises total cost: dist(player→node) + dist(node→endNode).
            // This avoids the "nearest node is behind/off to the side" trap that caused wrong
            // initial directions even when the true path is straight ahead.
            WaypointNode bestStart = FindBestStartNode(fromPos, _cachedDistToEnd);

            // Apply hysteresis: only commit to the new start node if it is meaningfully cheaper.
            bestStart = StabilizeStartNode(fromPos, bestStart, _cachedDistToEnd);

            if (bestStart == null || bestStart == endNode) return (targetPos, targetPos);

            // Walk precomputed next-hop pointers — no path reconstruction needed.
            if (!_cachedNextHop.TryGetValue(bestStart, out var hop1)) return (null, null);

            Vector3 wp1 = hop1.transform.position;
            Vector3 wp2 = _cachedNextHop.TryGetValue(hop1, out var hop2)
                ? hop2.transform.position
                : targetPos;
            return (wp1, wp2);
        }

        /// <summary>
        /// Runs Dijkstra outward from <paramref name="sourceNode"/> on the undirected graph.
        /// Returns:
        ///   dist[X]    = shortest graph distance from X to sourceNode
        ///   nextHop[X] = the first node to visit when traveling from X toward sourceNode
        ///
        /// Because the graph is undirected, running from sourceNode outward is equivalent
        /// to computing all-pairs distances to sourceNode — a single O(V²) pass.
        /// </summary>
        private (Dictionary<WaypointNode, float> dist, Dictionary<WaypointNode, WaypointNode> nextHop)
            RunDijkstraFrom(WaypointNode sourceNode)
        {
            var dist    = new Dictionary<WaypointNode, float>(_nodes.Count);
            var nextHop = new Dictionary<WaypointNode, WaypointNode>(_nodes.Count);
            // List preserves _nodes insertion order for deterministic tie-breaking.
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
                        // nextHop[neighbor] = current: "from neighbor, step to current, toward sourceNode"
                        nextHop[neighbor] = current;
                    }
                }
            }

            return (dist, nextHop);
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

        /// <summary>
        /// Returns the node that minimises total path cost: Euclidean distance from the
        /// player to that node, plus the Dijkstra distance from that node to the target.
        /// This correctly handles cases where the geometrically nearest node is off to the
        /// side or behind the player, which would cause the arrow to point the wrong way.
        /// </summary>
        private WaypointNode FindBestStartNode(Vector3 playerPos, Dictionary<WaypointNode, float> distToEnd)
        {
            WaypointNode best = null;
            float bestCost = float.MaxValue;
            foreach (var node in _nodes)
            {
                if (node == null) continue;
                if (!distToEnd.TryGetValue(node, out float toEnd) || toEnd == float.MaxValue)
                    continue; // unreachable node
                float total = Vector3.Distance(playerPos, node.transform.position) + toEnd;
                if (total < bestCost) { bestCost = total; best = node; }
            }
            return best;
        }

        /// <summary>
        /// Applies cost-based hysteresis to start node selection. Only switches away from
        /// the current start node if the candidate offers a meaningfully lower total cost
        /// (by at least <see cref="StartNodeSwitchThreshold"/> world units). This prevents
        /// the selected start node from oscillating when the player is near the crossover
        /// point between two equally-good nodes.
        /// </summary>
        private WaypointNode StabilizeStartNode(Vector3 playerPos, WaypointNode candidate,
            Dictionary<WaypointNode, float> distToEnd)
        {
            if (_lastStartNode == null || candidate == _lastStartNode)
            {
                _lastStartNode = candidate;
                return candidate;
            }

            float CostOf(WaypointNode n) =>
                Vector3.Distance(playerPos, n.transform.position)
                + (distToEnd.TryGetValue(n, out float d) ? d : float.MaxValue);

            if (CostOf(candidate) + StartNodeSwitchThreshold < CostOf(_lastStartNode))
            {
                _lastStartNode = candidate;
                return candidate;
            }
            return _lastStartNode;
        }
    }
}
