using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenVideoButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Open);
    }

    public void Open()
    {
        SaveProject.SaveToMemory();
        SceneManager.LoadScene("Video/Scenes/Videos");
    }
}
