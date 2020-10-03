using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedAnimator : MonoBehaviour
{
    public List<Animator> animators = new List<Animator>();

    public void SetVisible(bool isVisible)
    {
        foreach (Animator animator in animators)
        {
            animator.GetComponent<SpriteRenderer>().enabled = isVisible;
        }
    }

    public void SetXScale(float xScale)
    {
        foreach (Animator animator in animators)
        {
            animator.transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
        }
    }

    public void SetFloat(string key, float value)
    {
        foreach (Animator animator in animators)
        {
            if (animator.runtimeAnimatorController != null)
            {
                animator.SetFloat(key, value);
            }
        }
    }

    public void SetTrigger(string key)
    {
        foreach (Animator animator in animators)
        {
            if (animator.runtimeAnimatorController != null)
            {
                animator.SetTrigger(key);
            }
        }
    }

    public float GetFloat(string key)
    {
        foreach (Animator animator in animators)
        {
            if (animator.runtimeAnimatorController != null)
            {
                return animator.GetFloat(key);
            }
        }

        return 0f;
    }
}
