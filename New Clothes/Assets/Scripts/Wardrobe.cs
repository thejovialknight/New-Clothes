﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    [Header("State Data")]
    public List<ClothingItem> unlocked = new List<ClothingItem>();

    public static Wardrobe Instance;

    public void SetItemFound(ClothingItem item)
    {
        foreach (ClothingItem i in unlocked)
        {
            if (i.tag == item.tag)
            {
                return;
            }
        }

        unlocked.Add(item);
        GameManager.Instance.SaveGame();

        if (ClothingDatabase.Instance.ItemFromTag(item.tag) == null)
        {
            Debug.LogError("Item '" + item.tag + "' not found in database! Data serialization won't work correctly!");
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        GameManager.onSave += OnSave;
        GameManager.onFileLoad += OnLoad;
    }

    void OnDisable()
    {
        GameManager.onSave -= OnSave;
        GameManager.onFileLoad -= OnLoad;
    }

    void OnSave()
    {
        List<string> tags = new List<string>();
        foreach (ClothingItem item in unlocked)
        {
            tags.Add(item.tag);
        }

        GameManager.Instance.save.unlockedItemTags = tags.ToArray();
    }

    void OnLoad()
    {
        Save save = GameManager.Instance.save;

        foreach (string tag in save.unlockedItemTags)
        {
            ClothingItem itemToAdd = ClothingDatabase.Instance.ItemFromTag(tag);
            if (itemToAdd != null)
            {
                unlocked.Add(itemToAdd);
            }
        }
    }
}