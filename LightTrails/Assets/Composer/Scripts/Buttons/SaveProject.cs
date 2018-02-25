using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveProject : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        if (Project.CurrentModel == null)
        {
            Project.CurrentModel = new Project()
            {
                Id = "Debug"
            };
        }
    }

    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SaveClicked);
    }

    private void SaveClicked()
    {
        _button.interactable = false;
        FindObjectOfType<Notification>().PlaySaveNotification(() => _button.interactable = true);

        if (Project.CurrentModel == null)
        {
            return;
        }

        SaveToMemory();
        FindObjectOfType<ScreenShot>().TakeProjectThumbnail = true;

        Project.CurrentModel.SaveToDisk();
    }

    public static void SaveToMemory()
    {
        var storedEffectState = new List<StoredParticleItem>();
        var storedImageState = new List<StoredImageItem>();

        var effectMenuItems = FindObjectsOfType<EffectMenuItem>();
        var imageMenuItem = FindObjectsOfType<ImageMenuItem>();

        foreach (var item in effectMenuItems)
        {
            storedEffectState.Add(item.GetEffectSaveState());
        }

        foreach (var item in imageMenuItem)
        {
            storedImageState.Add(item.GetImageSaveState());
        }

        Project.CurrentModel.Items = new StoredItems()
        {
            Recorder = FindObjectOfType<RecorderMenuItem>().GetSaveState(),
            Images = storedImageState.ToArray(),
            Effects = storedEffectState.ToArray()
        };
    }
}
