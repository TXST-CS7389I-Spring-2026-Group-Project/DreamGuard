using UnityEditor;
using UnityEngine;

namespace DreamGuard.Editor
{
    /// <summary>
    /// Editor keyboard shortcuts for building waypoint graphs faster.
    ///
    /// Ctrl+Alt+W — with a WaypointNode selected, creates a new sibling node at the same
    /// position (nudged slightly on the X axis) and links it bidirectionally with the
    /// selected node. The new node is selected immediately so you can move it into place.
    ///
    /// Ctrl+Alt+B — with exactly two WaypointNodes selected, inserts a new node at their
    /// midpoint. Any existing direct link between the two is removed and replaced with
    /// A ↔ new ↔ B. The new node is selected so you can reposition it.
    ///
    /// Ctrl+Alt+G — with two or more WaypointNodes selected, links every selected node
    /// to every other selected node bidirectionally.
    /// </summary>
    public static class WaypointNodeShortcuts
    {
        private const string MenuPathSibling = "DreamGuard/Waypoint/Add Sibling Node %&w";    // Ctrl+Alt+W
        private const string MenuPathBetween = "DreamGuard/Waypoint/Insert Node Between %&b"; // Ctrl+Alt+B
        private const string MenuPathLink    = "DreamGuard/Waypoint/Link Selected Nodes %&g"; // Ctrl+Alt+G

        // ── Add Sibling ────────────────────────────────────────────────────────

        [MenuItem(MenuPathSibling, validate = false)]
        private static void AddSiblingWaypointNode()
        {
            var selected = Selection.activeGameObject;
            if (selected == null) return;

            var sourceNode = selected.GetComponent<WaypointNode>();
            if (sourceNode == null) return;

            // Create sibling under the same parent
            var newGO = new GameObject("WaypointNode");
            Undo.RegisterCreatedObjectUndo(newGO, "Add Sibling Waypoint Node");

            newGO.transform.SetParent(selected.transform.parent, worldPositionStays: false);
            newGO.transform.position = selected.transform.position + new Vector3(0.5f, 0f, 0f);

            var newNode = Undo.AddComponent<WaypointNode>(newGO);

            // Link bidirectionally: new → source and source → new
            WaypointNodeEditor.AddBackLink(newNode, sourceNode);
            WaypointNodeEditor.AddBackLink(sourceNode, newNode);

            Selection.activeGameObject = newGO;
        }

        [MenuItem(MenuPathSibling, validate = true)]
        private static bool AddSiblingWaypointNodeValidate()
        {
            var go = Selection.activeGameObject;
            return go != null && go.GetComponent<WaypointNode>() != null;
        }

        // ── Insert Between ─────────────────────────────────────────────────────

        [MenuItem(MenuPathBetween, validate = false)]
        private static void InsertNodeBetween()
        {
            var gos = Selection.gameObjects;
            var nodeA = gos[0].GetComponent<WaypointNode>();
            var nodeB = gos[1].GetComponent<WaypointNode>();

            Undo.SetCurrentGroupName("Insert Waypoint Node Between");
            int group = Undo.GetCurrentGroup();

            // Remove the direct link between A and B (if one exists)
            WaypointNodeEditor.RemoveBackLink(nodeA, nodeB);
            WaypointNodeEditor.RemoveBackLink(nodeB, nodeA);

            // Create midpoint node as a sibling of nodeA
            var newGO = new GameObject("WaypointNode");
            Undo.RegisterCreatedObjectUndo(newGO, "Insert Waypoint Node Between");
            newGO.transform.SetParent(nodeA.transform.parent, worldPositionStays: false);
            newGO.transform.position = Vector3.Lerp(
                nodeA.transform.position, nodeB.transform.position, 0.5f);

            var midNode = Undo.AddComponent<WaypointNode>(newGO);

            // Link: A ↔ mid ↔ B
            WaypointNodeEditor.AddBackLink(nodeA, midNode);
            WaypointNodeEditor.AddBackLink(midNode, nodeA);
            WaypointNodeEditor.AddBackLink(midNode, nodeB);
            WaypointNodeEditor.AddBackLink(nodeB, midNode);

            Undo.CollapseUndoOperations(group);

            Selection.activeGameObject = newGO;
        }

        [MenuItem(MenuPathBetween, validate = true)]
        private static bool InsertNodeBetweenValidate()
        {
            var gos = Selection.gameObjects;
            return gos != null
                && gos.Length == 2
                && gos[0].GetComponent<WaypointNode>() != null
                && gos[1].GetComponent<WaypointNode>() != null;
        }

        // ── Link Selected ──────────────────────────────────────────────────────

        [MenuItem(MenuPathLink, validate = false)]
        private static void LinkSelectedNodes()
        {
            var gos = Selection.gameObjects;

            Undo.SetCurrentGroupName("Link Selected Waypoint Nodes");
            int group = Undo.GetCurrentGroup();

            for (int i = 0; i < gos.Length; i++)
            {
                var a = gos[i].GetComponent<WaypointNode>();
                for (int j = i + 1; j < gos.Length; j++)
                {
                    var b = gos[j].GetComponent<WaypointNode>();
                    WaypointNodeEditor.AddBackLink(a, b);
                    WaypointNodeEditor.AddBackLink(b, a);
                }
            }

            Undo.CollapseUndoOperations(group);
        }

        [MenuItem(MenuPathLink, validate = true)]
        private static bool LinkSelectedNodesValidate()
        {
            var gos = Selection.gameObjects;
            if (gos == null || gos.Length < 2) return false;
            foreach (var go in gos)
                if (go.GetComponent<WaypointNode>() == null) return false;
            return true;
        }
    }
}
