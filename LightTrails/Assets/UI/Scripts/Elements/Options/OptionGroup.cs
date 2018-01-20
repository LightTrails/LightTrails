using UnityEngine;

public class OptionGroup : MonoBehaviour
{
    private void Awake()
    {
        Initialize();
    }

    public void Test()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        GetComponentsInChildren<OptionItem>().ForEach(o => o.Initialize());
    }
}
