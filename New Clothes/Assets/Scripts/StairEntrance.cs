using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEntrance : MonoBehaviour, IInteractable
{
    public Stairs stairs;
    public bool isTop = true;

    public Vector3 interactionOffset;

    public void Interact(Transform other)
    {
        Climber climber = other.GetComponent<Climber>();
        if (climber.currentStairs == null)
        {
            climber.EnterStairs(stairs, isTop);
        }
        else
        {
            climber.ExitStairs(stairs, isTop);
        }
    }

    public bool IsInteractable(Transform other)
    {
        Climber climber = other.GetComponent<Climber>();
        if (climber != null)
        {
            return false;
        }

        return false;
    }

    public Vector3 InteractionPosition()
    {
        return transform.position + interactionOffset;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(InteractionPosition(), 0.1f);
    }
}
