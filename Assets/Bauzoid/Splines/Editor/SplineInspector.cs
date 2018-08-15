using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SplineSetup))]
public class SplineInspector : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        SplineSetup spline = target as SplineSetup;
        if (spline == null)
            return;

        if (GUILayout.Button("Connect ControlPoints"))
        {
            spline.ConnectPoints();
            EditorUtility.SetDirty(spline);
        }
    }


}
