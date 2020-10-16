using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackout : MonoBehaviour
{
    public float floorLevel = 1f;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float playerLevel = LevelManager.Instance.playerFloorLevel;

        float difference = Mathf.Abs(playerLevel - floorLevel);
        if (difference <= 1f)
        {
            float opacity = Mathf.Lerp(0f, 1f, difference);
            sr.color = new Color(1f, 1f, 1f, opacity);
        }
    }
}
