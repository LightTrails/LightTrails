using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AnimatedFloat : Animation<float>
{
    public override float CalculateTarget(float progress)
    {
        switch (UsedFunction)
        {
            case EaseFunction.Linear:
                return Ease.Linear(OldTarget, NewTarget, progress);
            case EaseFunction.EaseInOutQuart:
                return Ease.EaseInOutQuart(OldTarget, NewTarget, progress);
            default:
                return Ease.EaseInOutQuart(OldTarget, NewTarget, progress);
        }
    }
}

