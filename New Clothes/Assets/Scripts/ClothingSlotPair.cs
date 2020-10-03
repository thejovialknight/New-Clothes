using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClothingSlotPair
{
    public ClothingSlot slot;
    public ClothingItem item;

    public ClothingSlotPair(ClothingSlot slot, ClothingItem item)
    {
        this.slot = slot;
        this.item = item;
    }
}
