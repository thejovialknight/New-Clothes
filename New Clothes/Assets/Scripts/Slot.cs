using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClothingSlotPair
{
    public Slot slot;
    public Item item;

    public ClothingSlotPair(Slot slot, Item item)
    {
        this.slot = slot;
        this.item = item;
    }
}
