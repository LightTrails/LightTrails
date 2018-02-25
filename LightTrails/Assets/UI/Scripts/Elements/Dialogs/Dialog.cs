using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : AnimatedObject
{
    public Action _callback;

    public void SetupLabelsAndCallbacks(string confirmText, string cancelText, Action onClick)
    {
        _callback = onClick;

        var confirm = transform.Find("Confirm");
        confirm.GetComponentInChildren<Text>().text = confirmText.ToUpper();
        confirm.GetComponent<Button>().onClick.RemoveAllListeners();
        confirm.GetComponent<Button>().onClick.AddListener(ConfirmClick);

        var cancel = transform.Find("Cancel");
        cancel.GetComponent<Button>().onClick.RemoveAllListeners();
        cancel.GetComponent<Button>().onClick.AddListener(Close);
        cancel.GetComponentInChildren<Text>().text = cancelText.ToUpper();
    }

    new void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Close();
        }

        base.Update();
    }

    private void ConfirmClick()
    {
        if (_callback != null)
        {
            _callback();
        }

        Close();
    }

    public void Close()
    {
        GetComponentInChildren<AnimatedObject>()
            .MoveBetweenXValues(0, (Screen.width / 2 + 300), () =>
            {
                GetComponentInParent<Overlay>().HideOverlay();
                gameObject.SetActive(false);
            });
    }
}
