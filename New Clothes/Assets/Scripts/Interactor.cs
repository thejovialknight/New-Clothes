using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public virtual bool Interact(Transform target)
    {
        return false;
    }
}
