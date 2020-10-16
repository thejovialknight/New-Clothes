using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddOnStart : MonoBehaviour
{
    public Item itemToAdd;

    public bool add = false;

    // Update is called once per frame
    void Update()
    {
        if(add)
        {
            GetComponent<Inventory>().AddToInventory(itemToAdd);
            add = false;
        }
    }
}
