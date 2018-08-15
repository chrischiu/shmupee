using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Bauzoid/Splines/SplineSetup")]
[ExecuteInEditMode]
public class SplineSetup : MonoBehaviour
{
    [Serializable]
    public class Segment
    {
        public ControlPointSetup PointA;
        public ControlPointSetup PointB;

        public float Length;
        public float StartAccumDistance;

        public float EndAccumDistance
        {
            get { return (StartAccumDistance + Length); }
        }
    }

    public List<Segment> Segments = new List<Segment>();

    public void Awake()
    {
        ConnectPoints();
    }

    public void OnDrawGizmos()
    {
        foreach (Segment s in Segments)
        {
            Gizmos.color = Color.yellow;
            if ((s.PointA != null) && (s.PointB != null))
                Gizmos.DrawLine(s.PointA.transform.position, s.PointB.transform.position);
        }
    }

    public void ConnectPoints()
    {
        Debug.Log("Connecting Points");

        Segments.Clear();

        ControlPointSetup[] controlPoints = GetComponentsInChildren<ControlPointSetup>();

        float totalDistance = 0.0f;
        for (int i = 1; i < controlPoints.Length; i++)
        {
            ControlPointSetup cp1 = controlPoints[i - 1];
            ControlPointSetup cp2 = controlPoints[i];

            // calculate distance
            float distance = CalcDistance(cp1, cp2);

            Segment seg = new Segment()
            {
                PointA = cp1, PointB = cp2,
                Length = distance,
                StartAccumDistance = totalDistance,
            };
            
            Segments.Add(seg);
            totalDistance += distance;
        }
    }

    public float CalcDistance(ControlPointSetup cp1, ControlPointSetup cp2)
    {
        return (cp1.transform.position - cp2.transform.position).magnitude;
    }

    public int GetSegmentIndex(float distance, out float segmentDistance)
    {
        if (Segments.Count == 0)
        {
            segmentDistance = 0.0f;
            return -1;
        }

        for (int i = 0; i < Segments.Count; i++)
        {
            Segment seg = Segments[i];
            if (distance < seg.EndAccumDistance)
            {
                segmentDistance = (distance - seg.StartAccumDistance);
                return i;
            }
        }

        segmentDistance = Segments[Segments.Count-1].EndAccumDistance;
        return Segments.Count-1;
    }

    public Vector3 GetSegmentPosition(int whichSegment, float segmentDistance)
    {
        if ((whichSegment < 0) || (whichSegment >= Segments.Count))
            return Vector3.zero;

        Segment seg = Segments[whichSegment];
        float t = Mathf.Clamp01(segmentDistance/seg.Length);

        return Vector3.Lerp(seg.PointA.transform.position, seg.PointB.transform.position, t);
    }

    public float GetArcLength()
    {
        if (Segments.Count == 0)
            return 0.0f;

        return Segments[Segments.Count - 1].EndAccumDistance;
    }

    public void OnHandleChanged(int i)
    {
    }

}
