using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Transform currentHider;

    SpriteRenderer sr;

    [Header("Hide Positions")]
    public Vector3 entrancePositionOffset;
    public Vector3 exitPositionOffset;

    [Header("Sprites")]
    public Sprite closedSprite;
    public Sprite openSprite;
    public float openLength = 0.25f;

    [Header("Audio")]
    public AudioClip openSound;
    [Range(0f, 1f)]
    public float openVolume = 1f;
    public AudioClip closeSound;
    [Range(0f, 1f)]
    public float closeVolume = 1f;
    public AudioClip hideSound;
    [Range(0f, 1f)]
    public float hideVolume = 1f;
    public AudioClip exitSound;
    [Range(0f, 1f)]
    public float exitVolume = 1f;

    public bool Hide(Hider hider)
    {
        if (currentHider == null)
        {
            Enter(hider);
        }
        else
        {
            if (currentHider == hider.transform)
            {
                Exit(hider);
            }
            else
            {
                Debug.Log("Someone trying to hide where something already hiding!!");
                return false;
            }
        }

        return true;
    }

    public Vector3 Entrance()
    {
        return transform.position + entrancePositionOffset;
    }

    public Vector3 Exit()
    {
        return transform.position + exitPositionOffset;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Enter(Hider hider)
    {
        currentHider = hider.transform;
        hider.HideInSpot(Entrance());

        // AudioController.PlayClip(hideSound, hideVolume);

        StartCoroutine(OpenThenClose(hider, true));
    }

    void Exit(Hider hider)
    {
        currentHider = null;
        hider.ExitSpot(Exit());

        // AudioController.PlayClip(exitSound, exitVolume);

        StartCoroutine(OpenThenClose(hider, false));
    }

    IEnumerator OpenThenClose(Hider hider, bool isHiding)
    {
        sr.sprite = openSprite;

        // AudioController.PlayClip(openSound, openVolume);

        if (!isHiding)
        {
            hider.GetComponent<AdvancedAnimator>().SetVisible(true);
        }

        yield return new WaitForSeconds(openLength);

        sr.sprite = closedSprite;

        // AudioController.PlayClip(closeSound, closeVolume);

        if (isHiding)
        {
            hider.GetComponent<AdvancedAnimator>().SetVisible(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Entrance(), 0.25f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Exit(), 0.25f);
    }
}
