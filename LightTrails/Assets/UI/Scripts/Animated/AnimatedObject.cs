using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObject : MonoBehaviour
{
    internal List<Animation> Animations = new List<Animation>();

    public void Update()
    {
        foreach (var item in Animations.ToArray())
        {
            item.RunProgress(Time.deltaTime, gameObject);
            if (item.IsDone)
            {

                Animations.Remove(item);
            }
        }
    }

    public void EndAllAnimations(bool setEndState = true)
    {
        Animations.ForEach(x => x.SetEndState(gameObject));
        Animations.Clear();
    }

    public void AddAnimation(Animation animation)
    {
        animation.SetStartState(gameObject);
        Animations.Add(animation);
    }

    internal void MoveBetweenXValues(float from, float to, Action callBack = null)
    {
        var rectTransform = transform as RectTransform;
        var anchoredPosition = rectTransform.anchoredPosition;
        anchoredPosition.x = from;

        rectTransform.anchoredPosition = anchoredPosition;

        AddAnimation(new AnimatedMovement()
        {
            NewTarget = new Vector2(to, anchoredPosition.y),
            OldTarget = rectTransform.anchoredPosition,
            CallBack = callBack
        });
    }
}
