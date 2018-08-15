using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(CheckPointSystem))]
public class CheckPointSystemInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Setup Checkpoints"))
        {
            SetupCheckPoints();
        }
    }

    public void SetupCheckPoints()
    {
        CheckPointSystem cps = target as CheckPointSystem;
        if (cps == null)
            return;

        ControlPointSetup[] controlPoints = cps.GetComponentsInChildren<ControlPointSetup>();
        foreach (ControlPointSetup cp in controlPoints)
        {
            CheckPoint checkPoint = cp.gameObject.GetComponent<CheckPoint>();
            if (checkPoint == null)
            {
                checkPoint = cp.gameObject.AddComponent<CheckPoint>();
            }
        }

        cps.ConnectChildren();
    }

}
