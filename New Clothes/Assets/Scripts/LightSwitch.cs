using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Interactable, IInteractable
{
    public bool isAmbient = false;
    public bool isOn;
    public Sprite offSprite;
    public Sprite onSprite;
    public Room room;

    public Vector3 interactionOffset;

    public List<GameObject> objs = new List<GameObject>();

    SpriteRenderer sr;

    public void Interact(Transform other)
    {
        isOn = !isOn;
        ValidateOn();
    }

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

    void Start()
    {
        ValidateOn();
    }

    void ValidateOn()
    {
        if (isOn)
        {
            sr.sprite = onSprite;
        }
        else
        {
            sr.sprite = offSprite;
        }

        foreach (GameObject obj in objs)
        {
            if(isAmbient)
            {
                obj.SetActive(!isOn);
            }
            else
            {
                obj.SetActive(isOn);
            }
        }

        if(isAmbient)
        {
            room.isLightOn = isOn;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(InteractionPosition(), 0.1f);
    }
}
