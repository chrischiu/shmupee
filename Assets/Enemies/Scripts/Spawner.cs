using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/Spawner")]
public class Spawner : MonoBehaviour
{

    public Transform EnableTarget;
    public Texture2D Icon;

    public void Update()
    {
        if ((EnableTarget != null) && (!EnableTarget.gameObject.activeSelf))
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            if ((screenPoint.x >= 0) && (screenPoint.x < Camera.main.pixelWidth) &&
                (screenPoint.y >= 0) && (screenPoint.y < Camera.main.pixelHeight))
            {
                Debug.Log("Spawner Triggered!");

                EnableTarget.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (Icon)
            Gizmos.DrawIcon(transform.position, Icon.name, true);
    }
}
