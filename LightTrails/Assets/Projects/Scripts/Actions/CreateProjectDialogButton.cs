using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateProjectDialogButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenCreateButtonDialog);
    }

    public void OpenCreateButtonDialog()
    {
        var dialog = Resources.FindObjectsOfTypeAll<CreateProjectDialog>().First();
        dialog.Show(() =>
        {
            Project project = Project.CreateNew(dialog.GetProjectName(), null);
            project.SaveToDisk();
            FindObjectOfType<ProjectList>().CreateProjectItem(project);
        });
    }

    internal void SetEnabled(bool enabled)
    {
        GetComponent<Button>().interactable = !enabled;
    }
}
