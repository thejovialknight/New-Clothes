using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusInteractor : MonoBehaviour
{
    public List<Interactor> interactors = new List<Interactor>();

    bool previouslySelected = false;

    public Transform target;

    public Vector2 offset;
    public float radius;
    public LayerMask interactableMask;

    public Transform interactionIndicator;
    public float indicatorLerpSpeed = 5f;

    void Update()
    {
        if (LevelManager.Instance.InGameAndRunning())
        {
            CheckInteractions();
            HandleInteractions();
        }
        else
        {
            target = null;
            interactionIndicator.position = new Vector3(1000f, 1000f, 1000f);
        }
    }

    void CheckInteractions()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + offset, radius, interactableMask);
        Collider2D closest = null;
        foreach (Collider2D collider in colliders)
        {
            if (closest == null)
            {
                closest = collider;
            }

            if (Vector3.Distance(transform.position, collider.transform.position) < Vector3.Distance(transform.position, closest.transform.position))
            {
                closest = collider;
            }
        }

        if (closest != null)
        {
            target = closest.transform;

            if (previouslySelected)
            {
                interactionIndicator.position = Vector3.Lerp(interactionIndicator.position, new Vector3(closest.transform.position.x, -1f, transform.position.z), indicatorLerpSpeed * Time.deltaTime);
            }
            else
            {
                previouslySelected = true;
                interactionIndicator.position = new Vector3(closest.transform.position.x, -1f, transform.position.z);
            }
        }
        else
        {
            target = null;
            interactionIndicator.position = new Vector3(1000f, 1000f, 1000f);
            previouslySelected = false;
        }
    }

    void HandleInteractions()
    {
        if (Input.GetButtonDown("Interact") && target != null)
        {
            foreach (Interactor interactor in interactors)
            {
                if (interactor.Interact(target))
                {
                    return;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
    }
}
