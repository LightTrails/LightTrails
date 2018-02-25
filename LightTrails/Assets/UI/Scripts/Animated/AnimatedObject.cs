using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    internal List<Animation> Animations = new List<Animation>();

    public void Update()
    {
        foreach (var item in Animations.ToArray())
        {
            item.RunProgress(Time.deltaTime);
            if (item.IsDone)
            {
                Animations.Remove(item);
            }
        }
    }

    public void EndAllAnimations(bool setEndState = true)
    {
        Animations.ForEach(x => x.SetEndState());
        Animations.Clear();
    }

    public void AddAnimation(Animation animation)
    {
        animation.SetStartState();
        Animations.Add(animation);
    }

    public void PlayAnimationsInSequence(params Animation[] animations)
    {
        if (!animations.Any())
        {
            return;
        }

        for (int i = 0; i < animations.Length - 1; i++)
        {
            var current = animations[i];
            var next = animations[i + 1];
            current.Callback = () => { AddAnimation(next); };
        }

        AddAnimation(animations.First());
    }

    internal void MoveBetweenXValues(float from, float to, Action callBack = null)
    {
        var rectTransform = transform as RectTransform;
        var anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.x = from;

        rectTransform.anchoredPosition = anchoredPosition;

        AddAnimation(new AnimatedMovement(gameObject)
        {
            NewTarget = new Vector2(to, anchoredPosition.y),
            OldTarget = rectTransform.anchoredPosition,
            Callback = callBack
        });
    }
}
