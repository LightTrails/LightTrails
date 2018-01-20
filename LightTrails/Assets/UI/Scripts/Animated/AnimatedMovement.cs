using System;
using UnityEngine;

public class AnimatedMovement : Animation
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
        (gameObject.transform as RectTransform).anchoredPosition = EaseInOutQuart(OldTarget, NewTarget, Progress);

        if (Progress >= 1 && CallBack != null)
        {
            CallBack();
        }

        IsDone = Progress >= 1;
    }

    public static Vector2 EaseInOutQuart(Vector2 start, Vector2 end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }
}
