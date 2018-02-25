using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseVideoButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        SceneManager.LoadScene("Composer/Scenes/Editor");
    }
}


