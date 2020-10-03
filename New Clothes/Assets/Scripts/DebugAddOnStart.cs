using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddOnStart : MonoBehaviour
{
    public ClothingItem itemToAdd;

    public bool add = false;

    // Update is called once per frame
    void Update()
    {
        if(add)
        {
            GetComponent<ClothingInventory>().AddToInventory(itemToAdd);
            add = false;
        }
    }
}
