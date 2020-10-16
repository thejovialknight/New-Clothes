using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusInteractor : MonoBehaviour
{
    // public List<Interactor> interactors = new List<Interactor>();
    public bool isMouseControlled = true;
    public List<Transform> targetingDots = new List<Transform>();

    public IInteractable target;
    public Transform targetTransform;
    public StairEntrance currentStairEntrance;

    public Vector2 offset;
    public float radius;
    public LayerMask interactableMask;

    public float indicatorLerpSpeed = 5f;

    void Update()
    {
        if (LevelManager.Instance.InGameAndRunning())
        {
            CheckInteractions();
            // HandleInteractions();
            SetTargetingDots();
        }
        else
        {
            target = null;
            SetDotsActive(false);
        }
    }

    void CheckInteractions()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + offset, radius, interactableMask);

        currentStairEntrance = null;
        Collider2D closest = null;
        IInteractable closestInteractable = null;
        foreach (Collider2D collider in colliders)
        {
            // check for stair entrance
            StairEntrance stair = collider.GetComponent<StairEntrance>();
            if(stair != null)
            {
                currentStairEntrance = stair;
            }

            IInteractable interactable = collider.GetComponent<IInteractable>();
            if (closest == null && interactable != null && interactable.IsInteractable(transform))
            {
                closest = collider;
                closestInteractable = interactable;
            }

            if (interactable != null && interactable.IsInteractable(transform) && Vector3.Distance(InteractorPosition(), interactable.InteractionPosition()) < Vector3.Distance(InteractorPosition(), closestInteractable.InteractionPosition()))
            {
                closest = collider;
                closestInteractable = interactable;
            }
        }

        if (closest != null)
        {
            if (closestInteractable != null)
            {
                target = closestInteractable;
                targetTransform = closest.transform;
                GameCursor.Instance.SetTarget(closestInteractable);
            }
            else
            {
                Debug.LogError("Object '" + closest.name + "' is tagged as interactable but has no components which derive from IInteractable!");
                Debug.Break();
            }
        }
        else
        {
            GameCursor.Instance.SetTarget(null);
            target = null;
            targetTransform = null;
        }
    }

    public bool Interact()
    {
        if (target != null)
        {
            if(target.IsInteractable(transform))
            {
                target.Interact(transform);
                return true;
            }
        }

        return false;
    }

    void SetTargetingDots()
    {
        if (target != null)
        {
            for (int i = 0; i < targetingDots.Count; i++)
            {
                targetingDots[i].position = Vector3.Lerp(InteractorPosition(), target.InteractionPosition(), (i + 1f) / (float)targetingDots.Count);
            }

            SetDotsActive(true);
        }
        else
        {
            SetDotsActive(false);
        }
    }

    void SetDotsActive(bool areActive)
    {
        foreach (Transform dot in targetingDots)
        {
            dot.gameObject.SetActive(areActive);
        }
    }

    Vector3 InteractorPosition()
    {
        if (isMouseControlled)
        {
            return GameCursor.WorldPosition();
        }
        else
        {
            return transform.position;
        }
    } 

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }
}
