using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamGuard.Editor
{
    /// <summary>
    /// Custom inspector for WaypointNode that enforces bidirectional neighbor links.
    ///
    /// Whenever a neighbor is added or removed in the Inspector, the reverse link on
    /// the other node is automatically added or removed so the neighbors list is always
    /// symmetric — no manual back-linking required.
    /// </summary>
    [CustomEditor(typeof(WaypointNode))]
    public class WaypointNodeEditor : UnityEditor.Editor
    {
        private List<WaypointNode> _prevNeighbors = new();

        private void OnEnable()
        {
            _prevNeighbors = ReadNeighbors(serializedObject);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            bool changed = EditorGUI.EndChangeCheck();

            if (changed)
            {
                serializedObject.ApplyModifiedProperties();
                SyncBidirectional();
                _prevNeighbors = ReadNeighbors(serializedObject);
            }
        }

        private void SyncBidirectional()
        {
            var current = ReadNeighbors(serializedObject);
            var self = (WaypointNode)target;

            foreach (var n in current)
            {
                if (n != null && !_prevNeighbors.Contains(n))
                    AddBackLink(n, self);
            }

            foreach (var n in _prevNeighbors)
            {
                if (n != null && !current.Contains(n))
                    RemoveBackLink(n, self);
            }
        }

        // ── Helpers ────────────────────────────────────────────────────────────

        private static List<WaypointNode> ReadNeighbors(SerializedObject so)
        {
            so.Update();
            var prop = so.FindProperty("neighbors");
            var list = new List<WaypointNode>(prop.arraySize);
            for (int i = 0; i < prop.arraySize; i++)
                list.Add(prop.GetArrayElementAtIndex(i).objectReferenceValue as WaypointNode);
            return list;
        }

        /// <summary>Adds <paramref name="to"/> to <paramref name="from"/>'s neighbor list if not already present.</summary>
        internal static void AddBackLink(WaypointNode from, WaypointNode to)
        {
            if (from == null || to == null || from == to) return;
            var so = new SerializedObject(from);
            var prop = so.FindProperty("neighbors");
            for (int i = 0; i < prop.arraySize; i++)
                if (prop.GetArrayElementAtIndex(i).objectReferenceValue == to) return; // already linked
            prop.arraySize++;
            prop.GetArrayElementAtIndex(prop.arraySize - 1).objectReferenceValue = to;
            so.ApplyModifiedProperties();
            EditorUtility.SetDirty(from);
        }

        /// <summary>Removes <paramref name="to"/> from <paramref name="from"/>'s neighbor list.</summary>
        internal static void RemoveBackLink(WaypointNode from, WaypointNode to)
        {
            if (from == null || to == null) return;
            var so = new SerializedObject(from);
            var prop = so.FindProperty("neighbors");
            for (int i = prop.arraySize - 1; i >= 0; i--)
            {
                if (prop.GetArrayElementAtIndex(i).objectReferenceValue == to)
                {
                    prop.DeleteArrayElementAtIndex(i);
                    so.ApplyModifiedProperties();
                    EditorUtility.SetDirty(from);
                    return;
                }
            }
        }
    }
}
