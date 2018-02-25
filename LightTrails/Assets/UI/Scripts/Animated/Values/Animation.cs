using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Animation
{
    public float Duration = 260.0f;
    public float Progress = 0;

    public bool IsDone;

    public enum EaseFunction { Linear, EaseInOutQuart }
    public EaseFunction UsedFunction = EaseFunction.EaseInOutQuart;
    public Action Callback;

    public abstract void ProgressUpdated(float newProgress);

    public void RunProgress(float deltaTime)
    {
        if (IsDone)
        {
            return;
        }

        Progress += Time.deltaTime * 1000.0f / Duration;
        ProgressUpdated(Progress);

        IsDone = Progress >= 1;

        if (IsDone && Callback != null)
        {
            Callback();
        }
    }

    public Animation SetStartState()
    {
        RunProgress(0);
        return this;
    }

    public Animation SetEndState()
    {
        Progress = 1;
        RunProgress(0);
        return this;
    }
}
