using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    CharacterEngine engine;

    void Awake()
    {
        engine = GetComponent<CharacterEngine>();
    }

    void OnEnable()
    {
        LevelManager.onInit += OnLoad;
    }

    void OnDisable()
    {
        LevelManager.onInit -= OnLoad;
    }

    void Update()
    {
        if(!LevelManager.Instance.pauseInfo.isInputPaused)
        {
            HandleInput();
        }
        else
        {
            engine.HaltJump();
            engine.SetHorizontalMovement(0f);
        }
    }

    void OnLoad()
    {
        LevelManager.Instance.RegisterPlayerTransform(transform);
    }

    void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            engine.Jump();
        }

        engine.SetHorizontalMovement(Input.GetAxisRaw("Horizontal"));
    }
}
