using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealthModifier
{
    public string tag;
    public float value;

    public StealthModifier(string tag, float value)
    {
        this.tag = tag;
        this.value = value;
    }
}
