using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    RadiusInteractor interactor;
    CharacterEngine engine;
    Climber climber;

    void Awake()
    {
        interactor = GetComponent<RadiusInteractor>();
        engine = GetComponent<CharacterEngine>();
        climber = GetComponent<Climber>();
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
            //engine.Jump();
        }

        if(Input.GetButtonDown("Interact"))
        {
            interactor.Interact();
        }

        engine.SetHorizontalMovement(Input.GetAxisRaw("Horizontal"));
        climber.SetVerticalMovement(Input.GetAxisRaw("Vertical"));
        climber.SetHorizontalMovement(Input.GetAxisRaw("Horizontal"));
    }
}
