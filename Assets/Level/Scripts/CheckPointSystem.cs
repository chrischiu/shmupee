using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Shmupee/CheckPointSystem")]
[RequireComponent(typeof(SplineSetup))]
public class CheckPointSystem : MonoBehaviour
{
    public Camera GameCamera;

    public float OffsetZ = -10;

    private SplineSetup mSpline;

    private List<CheckPoint> mCheckPoints;
    private int mCurrentCheckPoint = 0;

    private float mCurrentTime = 0.0f;
    private float mCurrentDistance = 0.0f;

    public void Awake()
    {
        // set up internal members
        mSpline = GetComponent<SplineSetup>();
        
        mCheckPoints = new List<CheckPoint>(GetComponentsInChildren<CheckPoint>());
        ConnectChildren();
    }

    public void Start()
    {
        if (mCheckPoints == null)
            return;

        mCurrentCheckPoint = 0;
        mCurrentTime = 0.0f;
        mCurrentDistance = 0.0f;
        GameCamera.transform.position = GetCurrentPosition();
    }

    public void Update()
    {
        mCurrentTime += Time.deltaTime;

        GameCamera.transform.position = GetCurrentPosition();
        mCurrentDistance += Time.deltaTime*GetCurrentSpeed();
    }

    public void ConnectChildren()
    {
        Debug.Log("Setting up CheckPoint System");

        if (mSpline == null)
            return;
    }

    public bool HasPosition()
    {
        if (mCheckPoints == null)
            return false;

        return true;
    }

    public float GetCurrentSpeed()
    {
        if (mCheckPoints == null)
            return 0.0f;

        if (mCurrentCheckPoint >= mCheckPoints.Count)
            return 0.0f;

        return mCheckPoints[mCurrentCheckPoint].ScrollSpeed;
    }

    public Vector3 GetCurrentPosition()
    {
        if (!HasPosition())
            return Vector3.zero;

        float segmentDistance;
        int whichSegment = mSpline.GetSegmentIndex(mCurrentDistance, out segmentDistance);

        Vector3 result = mSpline.GetSegmentPosition(whichSegment, segmentDistance);
        result.z += OffsetZ;
        return result;
    }


    
}
