using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderPreview : MonoBehaviour
{
    public static Texture Texture;
    public bool RunPreview = false;
    public float time;

    public void SetMaterial(Effect effect)
    {
        var material = Resources.Load<Material>("Materials/" + effect.Name);
        var rawImage = GetComponent<RawImage>();
        rawImage.material = material;

        if (Texture != null)
        {
            rawImage.texture = Texture;
        }
    }

    public void Reset()
    {
        time = 0;
        SetInputTime();
    }

    void Update()
    {
        if (!RunPreview)
        {
            return;
        }

        time += Time.deltaTime;
        SetInputTime();
    }

    private void SetInputTime()
    {
        GetComponent<RawImage>().material.SetFloat("_InputTime", time);
    }
}
