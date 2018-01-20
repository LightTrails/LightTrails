using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveDialog : Dialog
{
    private Action _onClick;
    public GameObject Overlay;

    public void Run()
    {
        SetupLabelsAndCallbacks("Save", "Cancel", () => { Debug.Log("Clicked"); });

        Overlay.GetComponent<Overlay>().Show();
        MoveBetweenXValues(-(Screen.width / 2 + 300), 0);


        gameObject.SetActive(true);
    }


}

