using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public List<string> tags = new List<string>();

    public bool Tag(string label)
    {
        foreach(string tag in tags)
        {
            if(label == tag)
            {
                return true;
            }
        }

        return false;
    }
}
