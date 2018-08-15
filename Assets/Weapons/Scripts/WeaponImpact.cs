using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/WeaponImpact")]
public class WeaponImpact : MonoBehaviour
{
    public void OnImpactAnimDone()
    {
        gameObject.SetActive(false);
    }
}
