using UnityEngine;

public class AttributeMenuItem : AnimatedObject
{

    public virtual void ReEvaluateEnabled()
    {

    }

    public static void RefreshButtonEnabledState()
    {
        foreach (var item in FindObjectsOfType<AttributeMenuItem>())
        {
            item.ReEvaluateEnabled();
        }
    }
}
