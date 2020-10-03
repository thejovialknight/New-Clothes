using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public string filename;
    public string[] unlockedItemTags;

    public Save(string filename)
    {
        this.filename = filename;
        unlockedItemTags = new string[] { };
    }
}