using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Assets.Models;

public class ParticleImagePreview : MonoBehaviour
{
    public void SetImage(Effect effect)
    {
        var parentName = gameObject.transform.parent.parent.gameObject.name;
        var loadedImage = Resources.Load<Texture2D>("Preview/" + parentName);
        if (loadedImage != null)
        {
            GetComponent<RawImage>().texture = loadedImage;
        }
    }
}
