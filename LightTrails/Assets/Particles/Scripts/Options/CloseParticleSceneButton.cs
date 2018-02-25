using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CloseParticleSceneButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Close);
    }

    public void Close()
    {
        SceneManager.LoadScene("Composer/Scenes/Editor");
    }
}


