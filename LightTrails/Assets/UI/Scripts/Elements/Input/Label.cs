using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Label : AnimatedObject
{
    public bool FromLeft = true;
    public float Duration = 150f;

    internal void FadeIn()
    {
        EndAllAnimations();

        var rectTransform = transform as RectTransform;
        var anchoredPosition = rectTransform.anchoredPosition;

        AddAnimation(new AnimatedTextTransparency()
        {
            NewTarget = 1.0f,
            OldTarget = 0.0f,
            Duration = Duration
        });

        Animations.Add(new AnimatedMovement()
        {
            NewTarget = anchoredPosition,
            OldTarget = anchoredPosition - new Vector2(FromLeft ? 30 : -30, 0),
            Duration = Duration
        });
    }
}
