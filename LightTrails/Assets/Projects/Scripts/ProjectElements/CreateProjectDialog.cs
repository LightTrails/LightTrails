using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateProjectDialog : Dialog
{
    public void Show(Action callBack)
    {
        SetupLabelsAndCallbacks("Create", "Cancel", callBack);

        var label = GetComponentInChildren<Label>();
        label.GetComponent<Text>().color = new Color(1, 1, 1, 0);

        Resources.FindObjectsOfTypeAll<Overlay>().First().GetComponent<Overlay>().Show();

        MoveBetweenXValues(-(Screen.width / 2 + 300), 0, () =>
        {
            label.FadeIn();
            GetComponentInChildren<InputField>().Select();
        });

        SetProjectName(string.Empty);
        gameObject.SetActive(true);
    }

    internal void SetProjectName(string name)
    {
        GetComponentInChildren<InputField>().text = name;
    }

    internal string GetProjectName()
    {
        return GetComponentInChildren<InputField>().text;
    }
}
