using System.Collections.Generic;
using UnityEngine;

namespace DreamGuard
{
    /// <summary>
    /// A single node in the waypoint graph used for maze navigation.
    ///
    /// Setup:
    ///   1. Add this component to an empty GameObject inside the maze.
    ///   2. Connect neighbors in the Inspector — link only nodes that share a clear
    ///      line-of-sight corridor (no wall between them).
    ///   3. Connections are one-way in the Inspector but the graph treats them as
    ///      bidirectional: if A lists B as a neighbor, the path A→B and B→A both work.
    ///
    /// At runtime the node has no renderer — it is invisible. Yellow gizmos are drawn
    /// in the editor (Scene view) to visualize the graph.
    /// </summary>
    public class WaypointNode : MonoBehaviour
    {
        [Tooltip("Marks this node as the room's exit point. " +
                 "When all orbs are collected the HUD arrow will guide the player here. " +
                 "Each WaypointGraph should have exactly one end node.")]
        [SerializeField] private bool isEnd = false;

        [Tooltip("Waypoint nodes directly reachable from this node without crossing a wall.")]
        [SerializeField] private List<WaypointNode> neighbors = new();

        public bool IsEnd => isEnd;
        public IReadOnlyList<WaypointNode> Neighbors => neighbors;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // End node: green filled sphere; regular node: yellow
            Gizmos.color = isEnd
                ? new Color(0.2f, 1f, 0.2f, 0.9f)
                : new Color(1f, 0.85f, 0f, 0.9f);
            Gizmos.DrawSphere(transform.position, isEnd ? 0.18f : 0.12f);

            // Lines to neighbors
            Gizmos.color = new Color(1f, 0.85f, 0f, 0.5f);
            if (neighbors == null) return;
            foreach (var neighbor in neighbors)
            {
                if (neighbor == null) continue;
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }

        private void OnDrawGizmosSelected()
        {
            // Highlight selected node and its neighbors
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 0.18f);

            if (neighbors == null) return;
            foreach (var neighbor in neighbors)
            {
                if (neighbor == null) continue;
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
                Gizmos.DrawWireSphere(neighbor.transform.position, 0.14f);
            }
        }
#endif
    }
}
