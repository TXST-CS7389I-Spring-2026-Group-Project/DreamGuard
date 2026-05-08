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
            EndNode = null;

            // Collect only this room's nodes (children of this GameObject)
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

            // Build bidirectional adjacency
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

            Debug.Log($"[WaypointGraph] Built graph on '{name}' — {_nodes.Count} nodes, endNode={(EndNode != null ? EndNode.name : "none")}");
        }

        /// <summary>
        /// Returns the world-space position of the next waypoint node along the
        /// shortest path from <paramref name="fromPos"/> to <paramref name="targetPos"/>.
        ///
        /// Returns null if the graph is empty or no path exists (caller falls back to
        /// pointing directly at the target).
        /// </summary>
        public Vector3? GetNextWaypointToward(Vector3 fromPos, Vector3 targetPos)
        {
            if (_nodes.Count == 0) return null;

            WaypointNode startNode = FindNearestNode(fromPos);
            WaypointNode endNode   = FindNearestNode(targetPos);

            if (startNode == null || endNode == null) return null;

            // Already at the same node — point directly
            if (startNode == endNode) return targetPos;

            // Dijkstra (O(n²) — fine for small maze graphs)
            var dist      = new Dictionary<WaypointNode, float>(_nodes.Count);
            var prev      = new Dictionary<WaypointNode, WaypointNode>(_nodes.Count);
            var unvisited = new HashSet<WaypointNode>(_nodes);

            foreach (var node in _nodes)
                dist[node] = float.MaxValue;
            dist[startNode] = 0f;

            while (unvisited.Count > 0)
            {
                WaypointNode current = null;
                float minDist = float.MaxValue;
                foreach (var node in unvisited)
                {
                    if (dist[node] < minDist) { minDist = dist[node]; current = node; }
                }

                if (current == null || current == endNode) break;

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
                        prev[neighbor] = current;
                    }
                }
            }

            if (!prev.ContainsKey(endNode)) return null;

            // Walk back from endNode to find the first step out of startNode
            var step = endNode;
            WaypointNode firstStep = endNode;
            while (prev.TryGetValue(step, out var parent))
            {
                if (parent == startNode) { firstStep = step; break; }
                firstStep = step;
                step = parent;
            }

            return firstStep.transform.position;
        }

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
