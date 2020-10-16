using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : Interactor
{
    /*
    Inventory inventory;

    public override bool Interact(Transform target)
    {
        Container container = target.GetComponent<Container>();
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
        inventory = GetComponent<Inventory>();
    }
    */
}
