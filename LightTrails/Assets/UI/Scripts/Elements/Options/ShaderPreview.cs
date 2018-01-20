using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderPreview : MonoBehaviour
{
    public bool RunPreview = false;
    public float time;

    // Update is called once per frame
    void Update()
    {
        if (!RunPreview)
        {
            return;
        }

        time += Time.deltaTime;
        GetComponent<RawImage>().material.SetFloat("_InputTime", time);
    }

}
