using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShaderEffectOption : OptionItem, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Foreground;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        animation.EndAllAnimations(false);
        animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.0f });

        var shaderPreview = GetComponentInChildren<ShaderPreview>();
        shaderPreview.RunPreview = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        animation.EndAllAnimations(false);
        animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.5f });

        var shaderPreview = GetComponentInChildren<ShaderPreview>();
        shaderPreview.RunPreview = false;
    }
}
