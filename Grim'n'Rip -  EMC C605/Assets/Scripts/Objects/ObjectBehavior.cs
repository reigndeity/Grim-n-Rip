using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    public Material materialObj;

    void Start()
    {
        if (materialObj != null)
        {
            // Ensure the material uses the Standard shader
            materialObj.shader = Shader.Find("Standard");
            SetMaterialToOpaque(materialObj); // Start with material in opaque mode
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Detected");
            SetMaterialToFade(materialObj, 155f / 255f);  // Change to Fade with alpha 155
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left");
            SetMaterialToOpaque(materialObj);  // Change to Opaque
        }
    }

    void SetMaterialToFade(Material mat, float alpha)
    {
        if (mat != null)
        {
            // Set rendering mode to Fade
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            // Set the alpha value
            Color color = mat.color;  // Get the current color
            color.a = alpha;          // Set the alpha
            mat.color = color;        // Apply the new color
        }
    }

    void SetMaterialToOpaque(Material mat)
    {
        if (mat != null)
        {
            // Set rendering mode to Opaque
            mat.SetFloat("_Mode", 0);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mat.SetInt("_ZWrite", 1);
            mat.EnableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;

            // Reset the alpha value to fully opaque (1)
            Color color = mat.color;
            color.a = 1f;  // Fully opaque
            mat.color = color; // Apply the new color
        }
    }
}
