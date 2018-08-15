using UnityEngine;
using System.Collections;

[AddComponentMenu("Shmupee/Weapons/SimpleShot")]
public class SimpleShot : MonoBehaviour
{
    public int NumShots = 5;
    public float CoolDownTime = 0.2f;
    public Vector3 ShotVelocity = new Vector3(10, 0, 0);

    public int NumImpacts = 6;

    public Transform ShotSprite;
    public Transform ShotParent;

    public Transform ImpactSprite;
    public Transform ImpactParent;

    public SpriteRenderer MuzzleFlash;

    public float WeaponDamage = 5;

    private Transform[] mShots;
    private Transform[] mImpacts;
    private Color mMuzzleFlashColor = new Color(1, 1, 1, 1);

    private float mCoolDownTimer = -1;

	public void Start()
	{
	    if (ShotSprite == null)
	        return;

	    mShots = new Transform[NumShots];
        for (int i = 0; i < mShots.Length; i++)
	    {
            mShots[i] = InstantiateShot();
	    }

	    mImpacts = new Transform[NumImpacts];
	    for (int i = 0; i < mImpacts.Length; i++)
	    {
	        mImpacts[i] = InstantiateImpact();
	    }

        if (MuzzleFlash != null)
	        MuzzleFlash.enabled = false;
	}
	
	public void Update()
	{
	    if (mCoolDownTimer > 0.0f)
	    {
	        mCoolDownTimer -= Time.deltaTime;

	        if ((MuzzleFlash != null) && (mCoolDownTimer <= 0.0f))
	        {
	            MuzzleFlash.enabled = false;
	        }
	    }

	    if (Input.GetButton("Jump") && (mCoolDownTimer <= 0.0f))
	    {
            for (int i = 0; i < mShots.Length; i++)
	        {
	            if (!mShots[i].gameObject.activeSelf)
	            {
	                mShots[i].transform.position = transform.position;
	                mShots[i].gameObject.SetActive(true);
	                mCoolDownTimer = CoolDownTime;

                    if (MuzzleFlash != null)
	                    MuzzleFlash.enabled = true;
	                break;
	            }
	        }
	    }

	    if ((MuzzleFlash != null) && (MuzzleFlash.enabled))
	    {
	        mMuzzleFlashColor.a = mCoolDownTimer/CoolDownTime;
	        MuzzleFlash.color = mMuzzleFlashColor;
	    }

        for (int i = 0; i < mShots.Length; i++)
        {
            if (mShots[i].gameObject.activeSelf)
            {
                mShots[i].transform.Translate(ShotVelocity * Time.deltaTime);
            }
        }
    }

    private Transform InstantiateShot()
    {
        Transform shot = Instantiate(ShotSprite);
        shot.tag = tag;
        if (ShotParent != null)
            shot.parent = ShotParent;
        shot.gameObject.SetActive(false);

        SimpleShotProjectile projectile = shot.GetComponent<SimpleShotProjectile>();
        if (projectile == null)
            projectile = shot.gameObject.AddComponent<SimpleShotProjectile>();

        projectile.Parent = this;

        return shot;
    }

    private Transform InstantiateImpact()
    {
        Transform impact = Instantiate(ImpactSprite);
        if (ImpactParent != null)
            impact.parent = ImpactParent;
        impact.gameObject.SetActive(false);
        return impact;
    }

    public void SpawnImpact(Transform obj)
    {
        Debug.Log("Spawn Impact");
        for (int i = 0; i < mImpacts.Length; i++)
        {
            if (!mImpacts[i].gameObject.activeSelf)
            {
                mImpacts[i].gameObject.SetActive(true);
                mImpacts[i].position = obj.position;
                
                break;
            }
        }
    }
}
