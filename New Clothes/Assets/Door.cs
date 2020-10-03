using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door otherSide;
    public Vector3 exitPositionOffset;
    public CameraBounds roomBounds;

    public Sprite closedSprite;
    public Sprite openedSprite;
    public float openLength = 0.25f;

    SpriteRenderer sr;

    public bool Enter()
    {
        StartCoroutine(OpenThenClose());
        StartCoroutine(otherSide.OpenThenClose());
        return true;
    }

    public Vector3 Exit()
    {
        return transform.position + exitPositionOffset;
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
}
