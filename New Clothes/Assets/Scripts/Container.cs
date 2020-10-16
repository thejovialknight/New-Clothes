using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{
    public Item contained;

    public Sprite closedSprite;
    public Sprite openedSprite;

    public Vector3 interactionOffset;

    bool isOpen = false;

    SpriteRenderer sr;

    // Attempts to open or close the container, returning true
    public void Interact(Transform other)
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            sr.sprite = openedSprite;

            // Add item to other inventory if available
            Inventory inventory = other.GetComponent<Inventory>();
            if (contained != null && inventory != null && !inventory.SlotFilled(contained))
            {
                inventory.AddToInventory(contained);
                contained = null;
            }
        }
        else
        {
            sr.sprite = closedSprite;
        }
    }

    // Always returns true.
    public bool IsInteractable(Transform other)
    {
        return true;
    }

    public Vector3 InteractionPosition()
    {
        return transform.position + interactionOffset;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        LevelManager.onInit += OnLoad;
    }

    void OnDisable()
    {
        LevelManager.onInit -= OnLoad;
    }

    void OnLoad()
    {
        LevelManager.Instance.RegisterContainer(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(InteractionPosition(), 0.1f);
    }
}
