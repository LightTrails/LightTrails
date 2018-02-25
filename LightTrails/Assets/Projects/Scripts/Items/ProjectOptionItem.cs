using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ProjectOptionItem : OptionItem, IPointerEnterHandler, IPointerExitHandler
{
    private Project _project;
    private bool _deleteMode;
    private DeleteModeToggleButton _deleteToggleButton;

    public GameObject Foreground;

    public Texture2D DefaultImage;

    public Color NormalModeColor;
    public Color DeleteModeColor;

    private void Start()
    {
        _deleteToggleButton = FindObjectOfType<DeleteModeToggleButton>();
        GetComponent<Button>().onClick.AddListener(ClickProjectItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        animation.EndAllAnimations(false);
        animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.0f });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var animation = Foreground.gameObject.GetComponent<AnimatedObject>();
        animation.EndAllAnimations(false);
        animation.AddAnimation(new AnimatedImageTransparency(Foreground) { NewTarget = 0.5f });
    }

    private void ClickProjectItem()
    {
        if (_deleteMode)
        {
            _project.Delete();
            FadeoutByResize();
        }
        else
        {
            Project.CurrentModel = _project;
            SceneManager.LoadScene("Composer/Scenes/Editor");
        }
    }

    public void SetProject(Project project)
    {
        _project = project;
        GetComponentInChildren<Text>().text = project.Name;

        var localProjectFile = _project.GetThumbnail();

        if (File.Exists(localProjectFile))
        {
            Texture2D tex = new Texture2D(0, 0);
            var bytes = File.ReadAllBytes(localProjectFile);
            if (bytes.Any())
            {
                tex.LoadImage(bytes);
                var rawImage = GetComponentInChildren<ProjectBackgroundImage>().GetComponent<RawImage>();
                rawImage.texture = tex;
                //rawImage.SizeToBounds(150.0f, 150.0f);
            }
        }
        else
        {
            var rawImage = GetComponentInChildren<ProjectBackgroundImage>().GetComponent<RawImage>();
            rawImage.texture = DefaultImage;
        }

        Initialize("Garbage");
    }

    void Update()
    {
        _deleteMode = _deleteToggleButton.DeleteMode;

        if (_deleteMode)
        {
            SetColorAndActive(DeleteModeColor, true);
        }
        else
        {
            SetColorAndActive(NormalModeColor, false);
        }
    }

    private void SetColorAndActive(Color color, bool active)
    {
        var projectBackground = GetComponentInChildren<ProjectBackgroundImage>();

        if (projectBackground != null)
        {
            projectBackground.GetComponent<RawImage>().color = color;
        }

        GetComponentInChildren<ProjectGarbageIcon>(true).gameObject.SetActive(active);
    }
}
