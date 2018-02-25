using Assets.Models;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ShaderOption : OptionItem, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Effect _effect;
    public GameObject Foreground;

    internal void Initialize(Effect effect)
    {
        _effect = effect;
        GetComponentInChildren<Text>().text = effect.Name;
        GetComponentInChildren<ShaderPreview>().SetMaterial(effect);
        Initialize();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemsMenu.EffectToAdd = _effect;
        FindObjectOfType<CloseShaderSceneButton>().Close();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        if (animation.enabled)
        {
            animation.EndAllAnimations(false);
            animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.0f });

            var shaderPreview = GetComponentInChildren<ShaderPreview>();
            if (shaderPreview != null)
            {
                shaderPreview.Reset();
                shaderPreview.RunPreview = true;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        if (animation.enabled)
        {
            animation.EndAllAnimations(false);
            animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.5f });

            var shaderPreview = GetComponentInChildren<ShaderPreview>();
            if (shaderPreview != null)
            {
                shaderPreview.RunPreview = false;
                shaderPreview.Reset();
            }
        }
    }
}
