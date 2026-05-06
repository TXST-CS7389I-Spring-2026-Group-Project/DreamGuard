using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DreamGuard.Editor
{
    [CustomEditor(typeof(Detection), true)]
    public class DetectionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Draw every field except targetObjects in its natural Inspector order
            var prop = serializedObject.GetIterator();
            prop.NextVisible(true); // position at m_Script

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(prop);
            EditorGUI.EndDisabledGroup();

            while (prop.NextVisible(false))
            {
                if (prop.name != "targetObjects")
                    EditorGUILayout.PropertyField(prop, true);
            }

            // Checkbox list for targetObjects
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Detection Targets", EditorStyles.boldLabel);

            var labelsAssetProp = serializedObject.FindProperty("labelsAsset");
            if (labelsAssetProp?.objectReferenceValue is not TextAsset ta)
            {
                EditorGUILayout.HelpBox(
                    "Assign a Labels Asset above to configure targets via checkboxes.",
                    MessageType.Info);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("targetObjects"), true);
                serializedObject.ApplyModifiedProperties();
                return;
            }

            string[] allLabels = ta.text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < allLabels.Length; i++)
                allLabels[i] = allLabels[i].Trim();

            var targetProp = serializedObject.FindProperty("targetObjects");
            var current = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < targetProp.arraySize; i++)
                current.Add(targetProp.GetArrayElementAtIndex(i).stringValue.Trim());

            bool changed = false;
            foreach (var label in allLabels)
            {
                bool was = current.Contains(label);
                bool now = EditorGUILayout.ToggleLeft(label, was);
                if (now != was)
                {
                    if (now) current.Add(label);
                    else current.Remove(label);
                    changed = true;
                }
            }

            if (changed)
            {
                // Rebuild array in label-file order
                var newList = new List<string>();
                foreach (var label in allLabels)
                    if (current.Contains(label))
                        newList.Add(label);

                targetProp.arraySize = newList.Count;
                for (int i = 0; i < newList.Count; i++)
                    targetProp.GetArrayElementAtIndex(i).stringValue = newList[i];
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
