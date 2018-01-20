using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSize : Animation
{
    public Vector2 NewTarget;
    public Vector2 OldTarget;

    public Action CallBack;

    public override void RunProgress(float deltaTime, GameObject gameObject)
    {
        if (IsDone)
        {
            return;
        }

        Progress += Time.deltaTime * 1000.0f / Duration;
        var current = Ease.EaseInOutQuart(OldTarget, NewTarget, Progress);
        (gameObject.transform as RectTransform).sizeDelta = current;

        IsDone = Progress >= 1;
        if (IsDone && CallBack != null)
        {
            CallBack();
        }
    }
}
