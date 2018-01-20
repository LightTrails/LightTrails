using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedTextTransparency : Animation
{
    public float NewTarget;
    public float OldTarget;

    public Action Callback;

    public override void RunProgress(float deltaTime, GameObject gameObject)
    {
        if (IsDone)
        {
            return;
        }

        Progress += Time.deltaTime * 1000.0f / Duration;

        var newColor = gameObject.GetComponent<Text>().color;
        newColor.a = Mathf.Lerp(OldTarget, NewTarget, Progress);
        gameObject.GetComponent<Text>().color = newColor;

        IsDone = Progress >= 1;

        if (IsDone && Callback != null)
        {
            Callback();
        }
    }
}
