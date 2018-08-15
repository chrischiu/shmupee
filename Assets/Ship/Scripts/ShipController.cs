using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/ShipController")]
public class ShipController : MonoBehaviour
{

    public Camera ScrollCamera;
    public float ShipSpeed;

    public float LimitMinX = 0.0f;
    public float LimitMaxX = 1.0f;
    public float LimitMinY = 0.0f;
    public float LimitMaxY = 1.0f;

    private ShipAnimations mShipAnimations = null;
    
    private Vector3 mLastCameraPos;

    private void Start()
    {
        mShipAnimations = GetComponent<ShipAnimations>();
        if (ScrollCamera)
            mLastCameraPos = ScrollCamera.transform.position;
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        float upDownAnim = 0.0f;

        Vector3 min = Camera.main.ScreenToWorldPoint(new Vector3(LimitMinX * (float)Camera.main.pixelWidth, LimitMinY * (float)Camera.main.pixelHeight, 0.0f));
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(LimitMaxX * (float)Camera.main.pixelWidth, LimitMaxY * (float)Camera.main.pixelHeight, 0.0f));

        if (moveX != 0.0f)
            moveX = ShipSpeed*Mathf.Sign(moveX);

        if (moveY != 0.0f)
        {
            moveY = ShipSpeed*Mathf.Sign(moveY);
            upDownAnim = Mathf.Sign(moveY);
        }

        // take into account camera scroll
        if (ScrollCamera)
        {
            moveX += ScrollCamera.transform.position.x - mLastCameraPos.x;
            moveY += ScrollCamera.transform.position.y - mLastCameraPos.y;

            mLastCameraPos = ScrollCamera.transform.position;
        }

        moveX = Mathf.Clamp(transform.position.x + moveX, min.x, max.x) - transform.position.x;
        moveY = Mathf.Clamp(transform.position.y + moveY, min.y, max.y) - transform.position.y;
        transform.Translate(moveX, moveY, 0.0f);

        if (mShipAnimations != null)
            mShipAnimations.UpDownTarget = upDownAnim;
    }
}

