using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : Interactor
{
    public bool isHidden;
    float gravityScale;

    RadiusInteractor interactor;
    AdvancedAnimator animator;
    Rigidbody2D rb;
    CharacterEngine engine;

    void Awake()
    {
        interactor = GetComponent<RadiusInteractor>();
        animator = GetComponent<AdvancedAnimator>();
        rb = GetComponent<Rigidbody2D>();
        engine = GetComponent<CharacterEngine>();
    }

    public override bool Interact(Transform target)
    {
        HidingSpot spot = target.GetComponent<HidingSpot>();

        if(spot != null)
        {
            spot.Hide(this);
            return true;
        }

        return false;
    }

    // returns true if hidden;
    public bool Hidden()
    {
        return isHidden;
    }

    public void HideInSpot(Vector3 entrancePosition)
    {
        transform.position = entrancePosition;
        gravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = Vector3.zero;
        engine.xMovement = 0f;
        engine.enabled = false;
        animator.SetTrigger("Hide");
        RaiseOnHideEnter();
        isHidden = true;
    }

    public void ExitSpot(Vector3 exitPosition)
    {
        transform.position = exitPosition;
        rb.gravityScale = gravityScale;
        engine.enabled = true;
        animator.SetTrigger("HideExit");
        RaiseOnHideExit();
        isHidden = false;
    }

    public delegate void OnHide();
    public event OnHide onHideEnter;
    void RaiseOnHideEnter()
    {
        if (onHideEnter != null)
        {
            onHideEnter();
        }
    }

    public event OnHide onHideExit;
    void RaiseOnHideExit()
    {
        if (onHideExit != null)
        {
            onHideExit();
        }
    }
}
