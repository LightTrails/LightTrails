using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenuAttribute(fileName = "ColorSchema", menuName = "ColorSchema", order = 0)]
public class ColorSchema : ScriptableObject
{
    public Color PrimaryColor;
    public Color PrimaryColorDark;
    public Color PrimaryColorLight;

    public Color SecondaryColor;
    public Color SecondaryColorDark;
    public Color SecondaryColorLight;

    public Color Disabled;

    public ColorBlock PrimaryBlock()
    {
        ColorBlock colorBlock = new ColorBlock
        {
            normalColor = PrimaryColor,
            pressedColor = Color.white,
            highlightedColor = PrimaryColorLight,
            disabledColor = Disabled,
            colorMultiplier = 1.0f,
            fadeDuration = 0.2f
        };
        return colorBlock;
    }

    public ColorBlock SecondaryBlock()
    {
        ColorBlock colorBlock = new ColorBlock
        {
            normalColor = SecondaryColor,
            pressedColor = Color.white,
            highlightedColor = SecondaryColorLight,
            disabledColor = Disabled,
            colorMultiplier = 1.0f,
            fadeDuration = 0.2f
        };
        return colorBlock;
    }
}
