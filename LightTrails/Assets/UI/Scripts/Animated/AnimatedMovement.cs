using System;
using UnityEngine;

public class AnimatedMovement : AnimatedVectorTwo
{
    public AnimatedMovement(GameObject gameObject)
    {
        TargetUpdated = newValue =>
        {
            (gameObject.transform as RectTransform).anchoredPosition = newValue;
        };
    }
}
