using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingDatabase : MonoBehaviour
{
    public List<ClothingItem> items = new List<ClothingItem>();

    public static ClothingDatabase Instance;

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

    public ClothingItem[] ItemsOfSlot(ClothingSlot slot)
    {
        List<ClothingItem> itemsOfSlot = new List<ClothingItem>();
        foreach(ClothingItem item in items)
        {
            if(item.slot == slot)
            {
                itemsOfSlot.Add(item);
            }
        }

        return itemsOfSlot.ToArray();
    }

    public ClothingItem ItemFromTag(string tag)
    {
        foreach (ClothingItem item in items)
        {
            if (item.tag == tag)
            {
                return item;
            }
        }

        Debug.LogError("No item found with name '" + tag + "'");
        return null;
    }
}
