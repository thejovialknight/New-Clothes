using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

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

    public Item[] ItemsOfSlot(Slot slot)
    {
        List<Item> itemsOfSlot = new List<Item>();
        foreach(Item item in items)
        {
            if(item.slot == slot)
            {
                itemsOfSlot.Add(item);
            }
        }

        return itemsOfSlot.ToArray();
    }

    public Item ItemFromTag(string tag)
    {
        foreach (Item item in items)
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
