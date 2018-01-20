using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionItemContentResizer : AnimatedObject
{
    public void FadeInByResize(params string[] hideChildren)
    {
        SetOverlayActive(true);
        SetOtherContentActive(false);

        var rect = transform.transform as RectTransform;

        AddAnimation(new AnimatedSize()
        {
            OldTarget = new Vector2(-200, -200),
            NewTarget = new Vector2(0, 0),
            Duration = 350,
            CallBack = () =>
            {
                SetOtherContentActive(true, hideChildren);
                GetComponentInChildren<OptionItemOverlay>().FadeOut(() =>
                {
                    SetOverlayActive(false);
                });
            }
        });
    }

    public void FadeOutByResize(Action callBack)
    {
        SetOverlayActive(true);
        SetOtherContentActive(false);

        var rect = transform.transform as RectTransform;

        AddAnimation(new AnimatedSize()
        {
            OldTarget = new Vector2(0, 0),
            NewTarget = new Vector2(-200, -200),
            Duration = 350,
            CallBack = () =>
            {
                callBack();
            }
        });
    }

    public void SetOverlayActive(bool active)
    {
        var overlay = transform.Find("Overlay").gameObject;
        if (active)
        {
            var currentColor = overlay.GetComponent<Image>().color;
            currentColor.a = 1.0f;
            overlay.GetComponent<Image>().color = currentColor;
        }

        overlay.SetActive(active);
    }

    public void SetOtherContentActive(bool active, params string[] exceptChildren)
    {
        foreach (Transform item in transform)
        {
            if (item.gameObject.name != "Overlay" && !exceptChildren.Any(x => x == item.gameObject.name))
            {
                item.gameObject.SetActive(active);
            }
        }
    }
}
