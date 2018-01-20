using System;
using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    public bool Open;

    public void Test()
    {
        GetComponentInChildren<SaveDialog>(true).Run();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void HideOverlay()
    {
        gameObject.SetActive(false);
    }
}
