using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ControlPointSetup))]
public class ControlPointInspector : Editor
{
    private const float HANDLE_SIZE = 0.1f;
    private const float PICK_SIZE = 0.2f;

    private Transform mHandleTransform;
    private Quaternion mHandleRotation;

    private int mSelectedHandle = -1;

    public override void OnInspectorGUI()
    {
        ControlPointSetup cp = target as ControlPointSetup;

        if (cp == null)
            return;

        base.OnInspectorGUI();
        
    }

    public void OnSceneGUI()
    {
        ControlPointSetup cp = target as ControlPointSetup;

        if (cp == null)
            return;

        Handles.color = Color.white;
        Handles.DrawLine(cp.transform.position, cp.transform.position + cp.HandleA);
        Handles.DrawLine(cp.transform.position, cp.transform.position + cp.HandleB);

        mHandleTransform = cp.transform;
        mHandleRotation = Tools.pivotRotation == PivotRotation.Local ? mHandleTransform.rotation : Quaternion.identity;

        for (int i = 0; i < ControlPointSetup.NUM_HANDLES; i++)
        {
            if (Handles.Button(cp.transform.position + cp.GetHandle(i), mHandleRotation, HANDLE_SIZE, PICK_SIZE, Handles.DotCap))
                mSelectedHandle = i;

            if (mSelectedHandle == i)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 point = Handles.DoPositionHandle(cp.transform.position + cp.GetHandle(i), mHandleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(cp, "Move Handle");
                    EditorUtility.SetDirty(cp);
                    cp.SetHandle(i, point - cp.transform.position);
                }
            }
        }
    }
}
