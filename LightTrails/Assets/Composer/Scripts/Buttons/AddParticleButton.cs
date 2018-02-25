using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddParticleButton : MonoBehaviour, IPointerClickHandler
{
    private Record recorder;

    private bool IsDisabled { get { return recorder.ActivelyRecording; } }

    public Attribute[] Attributes;

    private void Start()
    {
        recorder = FindObjectOfType<Record>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (IsDisabled)
        {
            return;
        }

        SaveProject.SaveToMemory();
        SceneManager.LoadScene("Particles/Scenes/ParticleOptions");
    }

    void Update()
    {
        GetComponent<Button>().interactable = !IsDisabled;
    }
}
