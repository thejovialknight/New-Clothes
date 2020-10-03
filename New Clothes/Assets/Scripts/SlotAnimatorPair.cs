using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlotAnimatorPair
{
    public ClothingSlot slot;
    public Animator animator;

    public SlotAnimatorPair(ClothingSlot slot, Animator animator)
    {
        this.slot = slot;
        this.animator = animator;
    }
}
