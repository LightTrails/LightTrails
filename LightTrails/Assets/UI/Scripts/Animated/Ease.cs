using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Ease
{
    public static Vector2 EaseInOutQuart(Vector2 start, Vector2 end, float value)
    {
        if (value >= 1) return end;
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }
}
