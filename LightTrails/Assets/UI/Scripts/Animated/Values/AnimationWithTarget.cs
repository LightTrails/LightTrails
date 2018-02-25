using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Animation<T> : Animation
{
    public Action<T> TargetUpdated;

    public T NewTarget;
    public T OldTarget;

    private T Current;

    public abstract T CalculateTarget(float progress);

    public override void ProgressUpdated(float newProgress)
    {
        Current = CalculateTarget(newProgress);
        if (TargetUpdated != null)
        {
            TargetUpdated(Current);
        }
    }
}
