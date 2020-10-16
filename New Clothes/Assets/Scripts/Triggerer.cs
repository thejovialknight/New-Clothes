using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggerer : Interactor
{
    public override bool Interact(Transform target)
    {
        ITriggerable triggerable = target.GetComponent<ITriggerable>();
        return triggerable.Trigger(transform);
    }
}
