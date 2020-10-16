using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    [Header("Hider Info")]
    public Transform currentHider;
    public float hiderGravityScale;

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

    public Vector3 interactionOffset;

    public void Interact(Transform other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        CharacterEngine eng = other.GetComponent<CharacterEngine>();
        AdvancedAnimator anim = other.GetComponent<AdvancedAnimator>();
        bool isHiding = false;

        // ENTER
        if (currentHider == null)
        {
            other.position = Entrance();
            hiderGravityScale = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = Vector3.zero;
            eng.xMovement = 0f;
            eng.enabled = false;
            anim.SetTrigger("Hide");
            LevelManager.Instance.playerStealth.SetModifier("Hidden", 100f);

            // RaiseOnHideEnter();
            // AudioController.PlayClip(hideSound, hideVolume);

            isHiding = true;
            currentHider = other;
        }
        // EXIT
        else
        {
            other.position = Exit();
            rb.gravityScale = hiderGravityScale;
            eng.enabled = true;
            anim.SetTrigger("HideExit");
            LevelManager.Instance.playerStealth.SetModifier("Hidden", 0f);

            // AudioController.PlayClip(exitSound, exitVolume);

            isHiding = false;
            currentHider = null;
        }

        StartCoroutine(OpenThenClose(other, isHiding));
    }

    public bool IsInteractable(Transform other)
    {
        if(currentHider == null || currentHider == other)
        {
            return true;
        }

        return false;
    }

    public Vector3 InteractionPosition()
    {
        return transform.position + interactionOffset;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    IEnumerator OpenThenClose(Transform other, bool isHiding)
    {
        sr.sprite = openSprite;

        // AudioController.PlayClip(openSound, openVolume);

        if (!isHiding)
        {
            other.GetComponent<AdvancedAnimator>().SetVisible(true);
        }

        yield return new WaitForSeconds(openLength);

        sr.sprite = closedSprite;

        // AudioController.PlayClip(closeSound, closeVolume);

        if (isHiding)
        {
            other.GetComponent<AdvancedAnimator>().SetVisible(false);
        }
    }

    Vector3 Entrance()
    {
        return transform.position + entrancePositionOffset;
    }

    Vector3 Exit()
    {
        return transform.position + exitPositionOffset;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Entrance(), 0.25f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Exit(), 0.25f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(InteractionPosition(), 0.1f);
    }
}
