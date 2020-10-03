using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : Interactor
{
    ClothingInventory inventory;

    public override bool Interact(Transform target)
    {
        ClothingContainer container = target.GetComponent<ClothingContainer>();
        if(container != null)
        {
            if(container.Open() && container.contained != null && inventory != null && !inventory.SlotFilled(container.contained))
            {
                inventory.AddToInventory(container.TakeItem());
            }
            return true;
        }

        return false;
    }

    void Awake()
    {
        inventory = GetComponent<ClothingInventory>();
    }
}
