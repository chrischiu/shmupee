using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[AddComponentMenu("Shmupee/ShipAnimations")]
public class ShipAnimations : MonoBehaviour
{
    public Sprite DefaultFrame;
    public List<Sprite> UpFrames = new List<Sprite>();
    public List<Sprite> DownFrames = new List<Sprite>();

    [Range(-1.0f, 1.0f)]
    public float UpDownTarget = 0.0f;

    public float FollowSpeed = 1.5f;

    public float FollowSpeedReset = 5.0f;

    public float CurrentUpDown = 0.0f;

    [Range(0.0f, 0.5f)] 
    public float DeadZone = 0.1f;

    private SpriteRenderer mSpriteRenderer;

    private void Start()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        if (mSpriteRenderer == null)
        {
            enabled = false;
        }
    }

    private void Update()
    {
        float snapSpeed = FollowSpeed;

        if (UpDownTarget == 0.0f)
            snapSpeed = FollowSpeedReset;

        if (UpDownTarget > CurrentUpDown)
        {
            CurrentUpDown = Mathf.Clamp(CurrentUpDown + Time.deltaTime * snapSpeed, -1.0f, UpDownTarget);
        }
        else if (UpDownTarget < CurrentUpDown)
        {
            CurrentUpDown = Mathf.Clamp(CurrentUpDown - Time.deltaTime * snapSpeed, UpDownTarget, 1.0f);
        }

        float upSteps = (1.0f - DeadZone) / (UpFrames.Count-1);
        float downSteps = (1.0f - DeadZone) / (DownFrames.Count-1);

        if (CurrentUpDown > DeadZone)
        {
            int index = Mathf.FloorToInt((CurrentUpDown - DeadZone) / upSteps);
            mSpriteRenderer.sprite = UpFrames[index];
        }
        else if (CurrentUpDown < (-DeadZone))
        {
            int index = Mathf.FloorToInt(-(CurrentUpDown + DeadZone) / downSteps);
            mSpriteRenderer.sprite = DownFrames[index];
        }
        else
        {
            mSpriteRenderer.sprite = DefaultFrame;
        }
    }
}

