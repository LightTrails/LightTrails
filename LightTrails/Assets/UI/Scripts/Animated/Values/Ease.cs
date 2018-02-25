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

    public static float EaseInOutQuart(float start, float end, float value)
    {
        if (value >= 1) return end;
        value /= .5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value * value * value + start;
        value -= 2;
        return -end * 0.5f * (value * value * value * value - 2) + start;
    }

    public static float Linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }

    public static Vector2 Linear(Vector2 start, Vector2 end, float value)
    {
        return Vector2.Lerp(start, end, value);
    }
}
