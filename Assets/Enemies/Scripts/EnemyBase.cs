using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/EnemyBase")]
public class EnemyBase : MonoBehaviour
{

    public const float DAMAGE_COOLDOWN = 0.3f;

    public float Health = 100;

    public Transform DestroyEffect;
    public float DestroyEffectScale = 2.0f;

    /*public SplineSetup MovePath;
    public float MoveSpeed = 1.0f;*/

    private SpriteRenderer mRenderer;
    private float mDamageCoolDown = -1;

    private Transform mDestroyEffectInstance;

    //private float mDistancePassed = 0.0f;

	void Start()
	{
	    mRenderer = GetComponent<SpriteRenderer>();

	    if (DestroyEffect != null)
	    {
	        mDestroyEffectInstance = Instantiate(DestroyEffect);
	        mDestroyEffectInstance.gameObject.SetActive(false);
	    }
	}
	
	void Update()
	{
	    if (mDamageCoolDown > 0.0f)
	    {
			mRenderer.material.SetFloat("_FlashAmount", Random.Range(0.0f, 1.0f));
			mDamageCoolDown -= Time.deltaTime;
	    }

	    if (mDamageCoolDown <= 0.0f)
	    {
			mRenderer.material.SetFloat("_FlashAmount", 0.0f);
	    }
	}

    public void ApplyDamage(float damage)
    {
        mDamageCoolDown = DAMAGE_COOLDOWN;

        // Apply Damage Effect
        DamageEffect damageeffect = Camera.main.GetComponent<DamageEffect>();
        damageeffect.StartDamageEffect();

        Health -= damage;
        if (Health <= 0)
        {
            Health = 0.0f;
            Die();
        }
    }

    public virtual void Die()
    {
        if (mDestroyEffectInstance != null)
        {
            mDestroyEffectInstance.position = transform.position;
            mDestroyEffectInstance.localScale = new Vector3(DestroyEffectScale, DestroyEffectScale, DestroyEffectScale);
            mDestroyEffectInstance.gameObject.SetActive(true);
        }

        gameObject.SetActive(false);
    }
}
