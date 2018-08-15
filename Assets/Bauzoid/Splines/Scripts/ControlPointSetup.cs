using UnityEngine;
using System.Collections;

[AddComponentMenu("Bauzoid/Splines/ControlPointSetup")]
public class ControlPointSetup : MonoBehaviour
{
    public const int NUM_HANDLES = 2;

    public const int HANDLE_A = 0;
    public const int HANDLE_B = 1;

    private Vector3[] Handles = { new Vector3(-1, 0, 0), new Vector3(1, 0, 0) };

    public Vector3 HandleA
    {
        get { return GetHandle(HANDLE_A); }
        set { SetHandle(HANDLE_A, value); }
    }

    public Vector3 HandleB
    {
        get { return GetHandle(HANDLE_B); }
        set { SetHandle(HANDLE_B, value); }
    }

    public Texture2D Icon;

    void OnDrawGizmos()
    {
        if (Icon)
            Gizmos.DrawIcon(transform.position, Icon.name, true);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(transform.position, transform.position + HandleA);
        Gizmos.DrawLine(transform.position, transform.position + HandleB);
    }

    public void OnHandleChanged(int i)
    {
        SplineSetup spline = GetComponentInParent<SplineSetup>();
        if (spline)
        {
            spline.OnHandleChanged(i);
        }
    }

    public Vector3 GetHandle(int i)
    {
        return Handles[i];
    }

    public void SetHandle(int i, Vector3 handle)
    {
        Handles[i] = handle;

        OnHandleChanged(i);
    }

}
