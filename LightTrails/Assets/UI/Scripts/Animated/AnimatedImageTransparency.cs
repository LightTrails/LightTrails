using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedImageTransparency : AnimatedFloat
{
    public AnimatedImageTransparency(GameObject gameObject)
    {
        UsedFunction = EaseFunction.Linear;

        TargetUpdated = newValue =>
        {
            var newColor = gameObject.GetComponent<Image>().color;
            newColor.a = newValue;
            gameObject.GetComponent<Image>().color = newColor;
        };
    }
}
