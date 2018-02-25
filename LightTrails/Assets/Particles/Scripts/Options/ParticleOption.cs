using Assets.Models;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class ParticleOption : OptionItem, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Effect _effect;
    public GameObject Foreground;

    internal void Initialize(Effect effect)
    {
        _effect = effect;
        GetComponentInChildren<Text>().text = effect.Name;
        GetComponentInChildren<ParticleImagePreview>().SetImage(effect);
        Initialize();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemsMenu.EffectToAdd = _effect;
        FindObjectOfType<CloseParticleSceneButton>().Close();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        animation.EndAllAnimations(false);
        animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.0f });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        animation.EndAllAnimations(false);
        animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.5f });
    }
}
