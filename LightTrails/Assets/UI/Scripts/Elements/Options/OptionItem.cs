using UnityEngine;
using System.Collections;
using System;

public class OptionItem : MonoBehaviour
{
    public void Initialize(params string[] hideChildren)
    {
        GetComponentInChildren<OptionItemContentResizer>().FadeInByResize(hideChildren);
    }

    public void FadeoutByResize()
    {
        GetComponentInChildren<OptionItemContentResizer>().FadeOutByResize(() =>
        {
            DestroyImmediate(this.gameObject);
        });
    }
}
