using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/SimpleShotProjectile")]
public class SimpleShotProjectile : MonoBehaviour
{
    public const float DEFAULT_DAMAGE = 5;

    public SimpleShot Parent;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            return;

        float damage = DEFAULT_DAMAGE;
        if (Parent != null)
        {
            Parent.SpawnImpact(transform);
            damage = Parent.WeaponDamage;
        }

        other.gameObject.SendMessage("ApplyDamage", damage);
        gameObject.SetActive(false);
    }
}
