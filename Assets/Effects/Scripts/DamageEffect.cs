using UnityEngine;
using System.Collections;

[AddComponentMenu("Effects/DamageEffect")]
public class DamageEffect : MonoBehaviour
{
    public bool Enabled = true;
    public Vector2 OffsetInitial = new Vector2(3.0f, 5.0f);

    public Texture2D NoiseTexture;

    public float Intensity;
    public float IntensityDecSpeed = 0.02f;
    public float MaxDistortion = 0.1f;

    private Material mEffectMaterial;
    private Vector2 mOffset;

    void Awake()
    {
        mEffectMaterial = new Material(Shader.Find("Custom/DamageEffect"));
    }


    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //Intensity = Mathf.Cos(Time.realtimeSinceStartup) * 0.5f + 0.5f;
        if (Intensity > 0.0f)
        {
            Intensity -= IntensityDecSpeed;
            if (Intensity < 0.0f)
            {
                Intensity = 0;
                Enabled = false;
            }
        }

        if (!Enabled)
        {
            Graphics.Blit(src, dest);
            return;
        }


        mOffset.x = OffsetInitial.x + Random.Range(-3.0f, 3.0f);
        mOffset.y = OffsetInitial.y + Random.Range(-3.0f, 3.0f);

        mEffectMaterial.SetFloat("_OffsetU", mOffset.x);
        mEffectMaterial.SetFloat("_OffsetV", mOffset.y);
        mEffectMaterial.SetFloat("_Intensity", Intensity);
        mEffectMaterial.SetFloat("_MaxDistortion", MaxDistortion);
        mEffectMaterial.SetTexture("_NoiseTex", NoiseTexture);
        Graphics.Blit(src, dest, mEffectMaterial);
    }


    /// <summary>
    /// Use the following lines to start the damage effect from anywhere (make sure you have added the DamageEffect to the main camera):
    /// 
    ///     DamageEffect damageeffect = Camera.main.GetComponent<DamageEffect>();
    ///     damageeffect.StartDamageEffect();
    ///     
    /// </summary>
    public void StartDamageEffect()
    {
        Intensity = 1.0f;
        Enabled = true;
    }

}
