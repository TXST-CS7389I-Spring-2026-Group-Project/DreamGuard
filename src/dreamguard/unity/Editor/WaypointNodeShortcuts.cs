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
    /// </summary>
    public static class WaypointNodeShortcuts
    {
        private const string MenuPath = "DreamGuard/Waypoint/Add Sibling Node %&w"; // Ctrl+Alt+W

        [MenuItem(MenuPath, validate = false)]
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

        [MenuItem(MenuPath, validate = true)]
        private static bool AddSiblingWaypointNodeValidate()
        {
            var go = Selection.activeGameObject;
            return go != null && go.GetComponent<WaypointNode>() != null;
        }
    }
}
