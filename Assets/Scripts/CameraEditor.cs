using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraController))]
public class CameraEditor : Editor
{
    private void OnSceneGUI()
    {
        CameraController camera = (CameraController)target;

        EditorGUI.BeginChangeCheck();
        Vector3 newBounds1 = Handles.PositionHandle(camera.Bounds1, Quaternion.identity);
        Vector3 newBounds2 = Handles.PositionHandle(camera.Bounds2, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(camera, "Change Camera Bounds");
            camera.Bounds1 = newBounds1;
            camera.Bounds2 = newBounds2;
        }
    }
}
