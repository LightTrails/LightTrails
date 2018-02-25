using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSize : AnimatedVectorTwo
{
    public AnimatedSize(GameObject gameObject)
    {
        TargetUpdated = newValue =>
        {
            (gameObject.transform as RectTransform).sizeDelta = newValue;
        };
    }
}
