using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SharkBehavior)), CanEditMultipleObjects]
public class SharkEditor : Editor {

    protected virtual void OnSceneGUI()
    {
        SharkBehavior example = (SharkBehavior)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition = Handles.PositionHandle(example.PatrolPoint, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(example, "Change Look At Target Position");
            example.PatrolPoint = newTargetPosition;
        }
    }
}
