using UnityEngine;
using UnityEditor;

public class AutoSnap : EditorWindow
{
    private Vector3 prevPosition;
    private Vector3 prevScale;
    private bool doSnap = true;
    private float snapValue = 0.5f;
    private float scaleSnapValue = 1f;

    [MenuItem("Edit/Auto Snap %_l")]

    static void Init()
    {
        var window = (AutoSnap) EditorWindow.GetWindow(typeof(AutoSnap));
        window.maxSize = new Vector2(200, 100);
    }

    public void OnGUI()
    {
        doSnap = EditorGUILayout.Toggle("Auto Snap", doSnap);
        snapValue = EditorGUILayout.FloatField("Snap Value", snapValue);
        scaleSnapValue = EditorGUILayout.FloatField("Scale Snap Value", scaleSnapValue);
    }

    public void Update()
    {
        if (doSnap
            && !EditorApplication.isPlaying
            && Selection.transforms.Length > 0
            && (Selection.transforms[0].position != prevPosition
            || Selection.transforms[0].localScale != prevScale))
        {
            Snap();
            prevPosition = Selection.transforms[0].position;
            prevScale = Selection.transforms[0].localScale;
        }
    }

    private void Snap()
    {
        foreach (var transform in Selection.transforms)
        {
            var p = transform.transform.position;
            p.x = Round(p.x, snapValue);
            p.y = Round(p.y, snapValue);
            p.z = Round(p.z, snapValue);
            transform.transform.position = p;

            var t = transform.transform.localScale;
            t.x = Round(t.x, scaleSnapValue);
            t.y = Round(t.y, scaleSnapValue);
            t.z = Round(t.z, scaleSnapValue);
            transform.transform.localScale = t;

        }
    }

    private float Round(float input, float snapVal)
    {
        return snapVal * Mathf.Round((input / snapVal));
    }
}