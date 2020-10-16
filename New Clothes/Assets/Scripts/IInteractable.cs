using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(Transform other);
    bool IsInteractable(Transform other);
    Vector3 InteractionPosition();
}