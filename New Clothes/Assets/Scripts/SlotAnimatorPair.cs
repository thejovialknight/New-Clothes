using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotAnimatorPair
{
    public Slot slot;
    public Animator animator;

    public SlotAnimatorPair(Slot slot, Animator animator)
    {
        this.slot = slot;
        this.animator = animator;
    }
}
