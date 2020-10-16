using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
    public Sprite normalCursorSprite;
    public Sprite targetCursorSprite;

    public IInteractable target;

    public static GameCursor Instance;

    SpriteRenderer sr;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position = WorldPosition();
        Cursor.visible = false;
    }

    public void SetTarget(IInteractable target)
    {
        this.target = target;

        if (target != null)
        {
            sr.sprite = targetCursorSprite;
        }
        else
        {
            sr.sprite = normalCursorSprite;
        }
    }

    public static Vector2 WorldPosition()
    {
        Camera cam = LevelManager.Instance.cam.cam;

        Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 worldPoint2d = new Vector2(worldPoint.x, worldPoint.y);

        return worldPoint2d;
    }
}
