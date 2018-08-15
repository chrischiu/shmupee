using UnityEngine;
using System.Collections;

[AddComponentMenu("BurutaruZ/Game/DisableOnExitScreen")]
public class DisableOnExitScreen : MonoBehaviour
{
    public int MarginOffset = 5;

    private Renderer mRenderer;

    public void Start()
    {
        mRenderer = GetComponent<Renderer>();
    }

	public void Update() 
	{
        if ((mRenderer != null) && (Camera.main != null))
	    {
	        Vector3 screenPointMin = Camera.main.WorldToScreenPoint(mRenderer.bounds.min);
            Vector3 screenPointMax = Camera.main.WorldToScreenPoint(mRenderer.bounds.min);

            if ((screenPointMax.x < -MarginOffset) || (screenPointMin.x > (Screen.width + MarginOffset)) ||
                (screenPointMax.y < -MarginOffset) || (screenPointMin.y > (Screen.height + MarginOffset)))
	        {
	            gameObject.SetActive(false);
	        }
	    }
	}
}
