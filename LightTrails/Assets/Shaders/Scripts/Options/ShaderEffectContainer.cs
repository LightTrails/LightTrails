using UnityEngine;
using System.Linq;
using Assets.Models;

public class ShaderEffectContainer : MonoBehaviour
{
    public GameObject Prefab;

    void Start()
    {
        foreach (var menuItem in GetComponentsInChildren<ShaderOption>())
        {
            DestroyObject(menuItem.gameObject);
        }

        foreach (var effect in EffectOptions.Options.Where(x => x.Type == Effect.EffectKind.Shader))
        {
            var newGameObject = Instantiate(Prefab);
            newGameObject.transform.SetParent(transform);
            newGameObject.name = effect.Name;
            newGameObject.GetComponent<ShaderOption>().Initialize(effect);
        }
    }
}
