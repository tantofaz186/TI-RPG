using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Objetos;
using Player;
using UnityEngine;

public class ExplodeParede : Interagível
{
    [SerializeField] GameObject[] peças;

    void Explode()
    {
        foreach(GameObject i in peças)
        {
            i.GetComponent<Rigidbody>().isKinematic = false;
            i.GetComponent<Rigidbody>().AddExplosionForce(100, transform.position, 10.0f, 2.0f);
        }

        transform.GetChild(14).gameObject.SetActive(false);
        StartCoroutine(MakeInvisibleThenDisable());
    }
    

    // Start is called before the first frame update
    void Start()
    {
        peças = new GameObject[gameObject.transform.childCount - 1];
        for(int i = 0; i < gameObject.transform.childCount - 1; i++)
        {
            peças[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    protected override void Interagir() => Explode();
    
    
    
    private IEnumerator MakeInvisibleThenDisable()
    {
        foreach (Material material in GetComponentsInChildren<Renderer>().Select((_renderer) => _renderer.material))
        {
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.SetInt("_Surface", 1);

            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            material.SetShaderPassEnabled("DepthOnly", false);
            material.SetShaderPassEnabled("SHADOWCASTER", false);

            material.SetOverrideTag("RenderType", "Transparent");

            material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
            material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        }

        
        float secondsUntilDisable = 5;
        while (secondsUntilDisable > 0)
        {
            foreach (Material material in GetComponentsInChildren<Renderer>().Select((_renderer) => _renderer.material))
            {
                if (material.HasProperty("_Color"))
                {
                    material.color = new Color(
                        material.color.r,
                        material.color.g,
                        material.color.b,
                        Mathf.Lerp(0, 1, secondsUntilDisable / 5)
                    );
                }
            }
            secondsUntilDisable -= Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
