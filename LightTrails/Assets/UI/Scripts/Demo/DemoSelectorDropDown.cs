using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DemoSelectorDropDown : MonoBehaviour
{
    public GameObject[] Elements;

    private Dropdown _dropDown;

    private void Start()
    {
        SetBySelectedValue(_dropDown.value);
    }

    private void Awake()
    {
        _dropDown = GetComponent<Dropdown>();
        _dropDown.ClearOptions();
        _dropDown.AddOptions(Elements.Select(x => x.name).ToList());
        _dropDown.onValueChanged.AddListener(SetBySelectedValue);
    }

    private void SetBySelectedValue(int newValue)
    {
        foreach (var item in Elements)
        {
            item.SetActive(false);
        }

        Elements[newValue].SetActive(true);
    }
}
