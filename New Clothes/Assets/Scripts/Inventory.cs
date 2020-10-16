using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ClothingSlotPair> inventory = new List<ClothingSlotPair>();
    public SpriteRenderer addedItemSpriteRenderer;
    public List<SlotAnimatorPair> animators = new List<SlotAnimatorPair>();

    AdvancedAnimator animator;

    void Awake()
    {
        animator = GetComponent<AdvancedAnimator>();
    }

    IEnumerator AddItemAnimation(Item item)
    {
        RaiseOnAddItem(item);
        animator.SetTrigger("Celebrate");
        addedItemSpriteRenderer.sprite = item.pickupSprite;
        LevelManager.Instance.SetPause(true);

        yield return new WaitForSeconds(1f);

        foreach (SlotAnimatorPair pair in animators)
        {
            if (pair.slot == item.slot)
            {
                pair.animator.runtimeAnimatorController = item.animator;
            }
        }
        addedItemSpriteRenderer.sprite = null;
        RaiseOnAddItemFinish();

        // TODO //
        // RELEGATE THIS TO SEPARATE CLASS SO THIS SCRIPT CAN BE USED WITH NPCS //
        // WIN CONDITIONS SHOULD ALWAYS BE SEPARATE CLASS //
        if (inventory.Count >= LevelManager.Instance.clothesRequiredToWin)
        {
            LevelManager.Instance.EndGame(true);
        }
        else
        {
            LevelManager.Instance.SetPause(false);
            animator.SetTrigger("CelebrateExit");
        }
    }

    public bool AddToInventory(Item item)
    {
        foreach (ClothingSlotPair pair in inventory)
        {
            if (item.slot == pair.slot)
            {
                return false;
            }
        }

        //AudioController.PlayClip(foundItemSound, 1f);
        inventory.Add(new ClothingSlotPair(item.slot, item));
        StartCoroutine(AddItemAnimation(item));

        Wardrobe.Instance.SetItemFound(item);
        return true;
    }

    public bool RemoveFromInventory(Item item)
    {
        ClothingSlotPair pairToRemove = null;
        foreach (ClothingSlotPair pair in inventory)
        {
            if (item.slot == pair.slot)
            {
                pairToRemove = pair;
            }
        }

        if (pairToRemove != null)
        {
            inventory.Remove(pairToRemove);
            RaiseOnRemoveItem(item);
            return true;
        }

        return false;
    }

    public bool SlotFilled(Item item)
    {
        foreach(ClothingSlotPair inventoryItem in inventory)
        {
            if(inventoryItem.slot == item.slot)
            {
                return true;
            }
        }

        return false;
    }

    public delegate void OnItem(Item item);
    public event OnItem onAddItem;
    public event OnItem onRemoveItem;

    public void RaiseOnAddItem(Item item)
    {
        if (onAddItem != null)
        {
            onAddItem(item);
        }
    }

    public void RaiseOnRemoveItem(Item item)
    {
        if (onRemoveItem != null)
        {
            onRemoveItem(item);
        }
    }

    public delegate void OnTrigger();
    public event OnTrigger onAddItemFinish;
    public void RaiseOnAddItemFinish()
    {
        if (onAddItemFinish != null)
        {
            onAddItemFinish();
        }
    }
}
