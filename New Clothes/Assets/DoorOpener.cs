using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : Interactor
{
    ClothingInventory inventory;

    public override bool Interact(Transform target)
    {
        Door door = target.GetComponent<Door>();
        if (door != null)
        {
            if(door.Enter())
            {
                transform.position = door.otherSide.Exit();
                CameraController.Instance.SetBounds(door.otherSide.roomBounds, true);
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
