using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingContainer : MonoBehaviour, IOpenable
{
    public ClothingItem contained;

    public Sprite closedSprite;
    public Sprite openedSprite;

    bool isOpen = false;

    SpriteRenderer sr;

    public ClothingItem TakeItem()
    {
        if(contained != null)
        {
            ClothingItem item = contained;
            contained = null;
            return item;
        }

        return null;
    }

    // Attempts to open or close the container, returning whether the container is open or closed
    public bool Open()
    {
        isOpen = !isOpen;
        if(isOpen)
        {
            sr.sprite = openedSprite;
        }
        else
        {
            sr.sprite = closedSprite;
        }

        return isOpen;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        LevelManager.onInit += OnLoad;
    }

    void OnDisable()
    {
        LevelManager.onInit -= OnLoad;
    }

    void OnLoad()
    {
        LevelManager.Instance.RegisterContainer(this);
    }
}
