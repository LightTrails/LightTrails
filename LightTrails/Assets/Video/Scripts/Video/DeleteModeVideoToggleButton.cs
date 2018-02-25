using System;
using UnityEngine;
using UnityEngine.UI;

public class DeleteModeVideoToggleButton : MonoBehaviour
{
    public bool DeleteMode;
    private Button _button;

    private ColorBlock _activeColorBlock;
    private ColorBlock _normalColorBlock;


    // Use this for initialization
    void Start()
    {
        _activeColorBlock = new ColorBlock()
        {
            normalColor = Color.red,
            pressedColor = Color.red,
            highlightedColor = Color.red,
            colorMultiplier = 1.0f
        };

        _button = GetComponent<Button>();
        _normalColorBlock = _button.colors;

        _button.onClick.AddListener(ToggleMode);
        SetButtonStates();
    }

    private void ToggleMode()
    {
        DeleteMode = !DeleteMode;
        SetButtonStates();
    }

    private void SetButtonStates()
    {
        if (DeleteMode)
        {
            _button.colors = _activeColorBlock;
        }
        else
        {
            _button.colors = _normalColorBlock;
        }
    }
}
