using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Door otherSide;
    public Vector3 exitPositionOffset;
    public CameraBounds roomBounds;
    public Room room;

    public Sprite closedSprite;
    public Sprite openedSprite;
    public float openLength = 0.25f;

    public Vector3 interactionOffset;

    SpriteRenderer sr;

    public void Interact(Transform other)
    {
        StartCoroutine(OpenThenClose());
        StartCoroutine(otherSide.OpenThenClose());

        other.position = Exit();
        LevelManager.Instance.playerLocation = otherSide.room;

        // REPLACE WITH CAMERA BOUNDS FROM ROOM. RATHER, CAMERA SHOULD ALWAYS KNOW THE BOUNDS OF THE ROOM IT IS IN
        CameraController.Instance.SetBounds(otherSide.roomBounds, true);
    }

    public bool IsInteractable(Transform other)
    {
        return true;
    }

    public Vector3 InteractionPosition()
    {
        return transform.position + interactionOffset;
    }

    public Vector3 Exit()
    {
        return otherSide.transform.position + otherSide.exitPositionOffset;
    }

    IEnumerator OpenThenClose()
    {
        sr.sprite = openedSprite;

        // AudioController.PlayClip(openSound, openVolume);

        yield return new WaitForSeconds(openLength);

        sr.sprite = closedSprite;

        // AudioController.PlayClip(closeSound, closeVolume);
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(InteractionPosition(), 0.1f);
    }
}
