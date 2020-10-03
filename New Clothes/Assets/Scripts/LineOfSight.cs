using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public LayerMask sightLayer;
    public Vector2 target;
    public Vector2 sightDirection;
    public float sightRange = 4f;

    [Header("Animator")]
    public Transform maxLOSIndicator;
    public Transform midLOSIndicator;
    public float lerpSpeed = 5f;

    [Header("State Data")]
    public List<Transform> objectsInView = new List<Transform>();
    public bool debugPlayerInSight;

    // Animator
    SpriteRenderer maxSr;
    SpriteRenderer midSr;

    void Awake()
    {
        maxSr = maxLOSIndicator.GetComponent<SpriteRenderer>();
        midSr = midLOSIndicator.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleController();
        HandleIndicator();
    }

    void HandleController()
    {
        sightDirection = (target - (Vector2)transform.position).normalized;
        // y goes crazy when x is near. don't know why.

        objectsInView.Clear();
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, sightDirection, sightRange, sightLayer);
        foreach (RaycastHit2D hit in hits)
        {
            objectsInView.Add(hit.transform);
        }

        debugPlayerInSight = PlayerInSight();
        Debug.DrawLine(transform.position, transform.position + (Vector3)sightDirection * sightRange, Color.red);

    }

    void HandleIndicator()
    {
        maxLOSIndicator.localPosition = Vector2.Lerp(maxLOSIndicator.localPosition, sightDirection * sightRange, lerpSpeed * Time.deltaTime);
        midLOSIndicator.localPosition = Vector2.Lerp(midLOSIndicator.localPosition, sightDirection * (sightRange / 2), lerpSpeed * Time.deltaTime);

        if (!LevelManager.Instance.InGameAndRunning())
        {
            maxSr.enabled = false;
            midSr.enabled = false;
        }
        else if (maxSr.enabled == false)
        {
            maxSr.enabled = true;
            midSr.enabled = true;
        }
    }

    // returns true if the player is in sight, false otherwise
    public bool PlayerInSight()
    {
        foreach (Transform obj in objectsInView)
        {
            if (obj.gameObject.tag == "Player")
            {
                if (!obj.GetComponent<Hider>().Hidden())
                {
                    return true;
                }
            }
        }
        return false;
    }
}
