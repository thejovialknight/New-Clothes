using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoor : Interactable, IInteractable
{
    public bool isOpen = false;
    public Sprite openedSprite;
    public Sprite closedSprite;

    public Vector3 interactionOffset;

    SpriteRenderer sr;
    Collider2D col;

    public void Interact(Transform other)
    {
        isOpen = !isOpen;
        VerifyOpen();
    }

    public bool IsInteractable(Transform other)
    {
        return true;
    }

    public Vector3 InteractionPosition()
    {
        return transform.position + interactionOffset;
    }

    void VerifyOpen()
    {
        if(isOpen)
        {
            sr.sprite = openedSprite;
            col.isTrigger = true;
        }
        else
        {
            sr.sprite = closedSprite;
            col.isTrigger = false;
        }
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        VerifyOpen();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(InteractionPosition(), 0.1f);
    }
}
