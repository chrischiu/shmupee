using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/MoveObject")]
public class MoveObject : MonoBehaviour 
{

    public SplineSetup MovePath;
    public float MoveSpeed = 1.0f;

    public float ZRotationStart = 0.0f;
    public float ZRotationEnd = 0.0f;

    private float mDistancePassed = 0.0f;


    public void Start()
    {
        mDistancePassed = 0.0f;
        ApplyPath();
    }

    public void Update()
    {
        mDistancePassed += Time.deltaTime * MoveSpeed;
        ApplyPath();
    }

    private void ApplyPath()
    {
        if (MovePath == null)
            return;

        float segmentDistance;
        int whichSegment = MovePath.GetSegmentIndex(mDistancePassed, out segmentDistance);
        transform.position = MovePath.GetSegmentPosition(whichSegment, segmentDistance);

        float t = Mathf.Clamp01(mDistancePassed/MovePath.GetArcLength());
        float angleRad = ZRotationStart + t*(ZRotationEnd - ZRotationStart);
        Vector3 angles = new Vector3(0, 0, angleRad);
        transform.eulerAngles = angles;
    }
}
