using Assets.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsMenu : MonoBehaviour
{
    public static int SelectedMenuItemIndex = -1;
    public bool SelectedMenuItemIndexHasInitialValue { get { return SelectedMenuItemIndex == -1; } }

    public static Effect EffectToAdd;

    public GameObject ParticleEffectPrefab;
    public GameObject ShaderEffectPrefab;
    public GameObject ImagePrefab;

    void Start()
    {
        LoadEffects();

        MaskPanel.EnsureLoaded();

        if (SelectedMenuItemIndexHasInitialValue)
        {
            SelectFirstItem();
        }
    }

    internal void SelectFirstItem()
    {
        ItemSelected(GetComponentsInChildren<MenuItem>().First());
    }

    internal void LoadEffects()
    {
        if (Project.CurrentModel != null)
        {
            var names = EffectOptions.Options.ToDictionary(x => x.Name);

            var storedMenuItems = new List<StoredIndexedItem>();
            storedMenuItems.AddRange(Project.CurrentModel.Items.Effects);
            storedMenuItems.AddRange(Project.CurrentModel.Items.Images);

            foreach (var item in storedMenuItems.OrderBy(x => x.Index))
            {
                if (item is StoredParticleItem)
                {
                    var effectItem = item as StoredParticleItem;
                    if (names.ContainsKey(effectItem.Name))
                    {
                        var menuItem = AddEffect(names[effectItem.Name]);
                        menuItem.SetEffectSaveState(effectItem);
                    }
                }

                if (item is StoredImageItem)
                {
                    var imageItem = item as StoredImageItem;
                    var image = AddImage();
                    image.SetImage(imageItem.ImagePath);
                    image.SetShader(imageItem.Shader);
                    image.SetSaveState(imageItem);
                }
            }

            var menuItems = GetMenuItems();
            if (SelectedMenuItemIndex != -1 && menuItems.Count > SelectedMenuItemIndex)
            {
                ItemSelected(menuItems[SelectedMenuItemIndex]);
            }
        }

        if (EffectToAdd != null)
        {
            AddEffect(EffectToAdd);
            EffectToAdd = null;
        }
    }

    public ImageMenuItem AddImage()
    {
        var newGameObject = Instantiate(ImagePrefab);
        newGameObject.transform.SetParent(transform);

        var image = FindObjectOfType<ImageList>().AddImage();
        var imageMenuItem = newGameObject.GetComponent<ImageMenuItem>();

        imageMenuItem.Initialize(image, imageMenuItem.transform.GetSiblingIndex());

        return imageMenuItem;
    }

    public ImageMenuItem GetSelectedImageMenuItem()
    {
        return GetComponentsInChildren<ImageMenuItem>().FirstOrDefault(x => x.Selected);
    }

    internal EffectMenuItem AddEffect(Effect effect)
    {
        if (effect.Type == Effect.EffectKind.Particle)
        {
            return SetParticleEffect(effect);
        }
        else if (effect.Type == Effect.EffectKind.Shader)
        {
            var selectedImageMenuItem = GetSelectedImageMenuItem();
            selectedImageMenuItem.SetShader(effect);
        }

        return null;
    }

    private EffectMenuItem SetParticleEffect(Effect effect)
    {
        GameObject newGameObject = null;
        EffectMenuItem effectMenuItem = null;

        newGameObject = Instantiate(ParticleEffectPrefab);
        newGameObject.transform.SetParent(transform);

        if (effect.MenuItemType != null)
        {
            effectMenuItem = newGameObject.AddComponent(effect.MenuItemType) as ParticleEffectMenuItem;
        }
        else
        {
            effectMenuItem = newGameObject.AddComponent<ParticleEffectMenuItem>();
        }

        newGameObject.name = effect.Name;

        effectMenuItem.Initialize(effect);
        effectMenuItem.SelectEffect();

        if (effectMenuItem is ParticleEffectMenuItem)
        {
            (effectMenuItem as ParticleEffectMenuItem).SetEffectBasedOnIndex(effectMenuItem.transform.GetSiblingIndex());
        }

        return effectMenuItem;
    }

    public void ItemSelected(MenuItem element)
    {
        var menuItems = GetMenuItems();
        SelectedMenuItemIndex = menuItems.IndexOf(element);

        if (element.Selected)
        {
            return;
        }

        MaskPanel.Close();

        foreach (var item in GetComponentsInChildren<MenuItem>())
        {
            if (item != element)
            {
                item.HasBeenUnSelected();
            }

        }

        element.HasBeenSelected();

        FindObjectOfType<AttributesMenu>().CreateProperties(element.GetAttributes());

        var draggableSystem = Resources.FindObjectsOfTypeAll<DraggableParticleSystem>().First();
        draggableSystem.gameObject.SetActive(false);
    }

    private List<MenuItem> GetMenuItems()
    {
        return GetComponentsInChildren<MenuItem>().ToList();
    }

    public EffectMenuItem GetSelectedItem()
    {
        return FindObjectsOfType<EffectMenuItem>().FirstOrDefault(x => x.Selected);
    }
}
