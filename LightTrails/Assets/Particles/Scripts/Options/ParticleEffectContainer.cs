using UnityEngine;
using System.Linq;
using Assets.Models;

public class ParticleEffectContainer : MonoBehaviour
{
    public GameObject Prefab;

    void Start()
    {
        foreach (var menuItem in GetComponentsInChildren<ParticleOption>())
        {
            DestroyObject(menuItem.gameObject);
        }

        foreach (var effect in EffectOptions.Options.Where(x => x.Type == Effect.EffectKind.Particle))
        {
            var newGameObject = Instantiate(Prefab);
            newGameObject.transform.SetParent(transform);
            newGameObject.name = effect.Name;
            newGameObject.GetComponent<ParticleOption>().Initialize(effect);
        }
    }
}
