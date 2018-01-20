using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedImageTransparency : Animation
{
    public float NewTarget;
    public float OldTarget;

    public Action Callback;

    public AnimatedImageTransparency(GameObject gameObject)
    {
        OldTarget = gameObject.GetComponent<Image>().color.a;
    }

    public AnimatedImageTransparency()
    {

    }

    public override void RunProgress(float deltaTime, GameObject gameObject)
    {
        if (IsDone)
        {
            return;
        }

        Progress += Time.deltaTime * 1000.0f / Duration;

        var newColor = gameObject.GetComponent<Image>().color;
        newColor.a = Mathf.Lerp(OldTarget, NewTarget, Progress);
        gameObject.GetComponent<Image>().color = newColor;

        IsDone = Progress >= 1;

        if (IsDone && Callback != null)
        {
            Callback();
        }
    }
}
