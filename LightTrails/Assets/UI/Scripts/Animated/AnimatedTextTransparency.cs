using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedTextTransparency : AnimatedFloat
{
    public AnimatedTextTransparency(GameObject gameObject)
    {
        UsedFunction = EaseFunction.Linear;

        TargetUpdated = newValue =>
        {
            var newColor = gameObject.GetComponent<Text>().color;
            newColor.a = Mathf.Lerp(OldTarget, NewTarget, Progress);
            gameObject.GetComponent<Text>().color = newColor;
        };
    }
}
