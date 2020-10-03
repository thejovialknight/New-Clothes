using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterEngine : MonoBehaviour
{
    public bool isNPC = true;
    public float savedXMovement = 0f;

    [Header("Movement")]
    public float runSpeed = 9f;
    public float jumpSpeed = 4f;
    public float groundCheckRayLength = 0.1f;
    public LayerMask groundCheckLayerMask;

    [Header("Animation")]
    public float timeBetweenSteps = 0.33f;

    [Header("Info")]
    public bool isGrounded = false;
    public bool isFalling = false;
    public bool hasJustJumped = false;
    public bool isReadyToJump = false;
    public float xMovement = 0f;
    public bool isOnGround
    {
        get
        {
            Vector3 position = transform.position;
            position.y = col.bounds.min.y + 0.1f;
            float length = groundCheckRayLength + 0.1f;
            Debug.DrawRay(position, Vector3.down * length);
            bool grounded = Physics2D.Raycast(position, Vector3.down, length, groundCheckLayerMask.value);
            return grounded;
        }
    }

    // Animation
    float currentTimeBetweenSteps = 0f;

    // Component References
    BoxCollider2D col;
    Rigidbody2D rb;
    AdvancedAnimator anim;

    public void SetHorizontalMovement(float movement)
    {
        xMovement = movement;
    }

    public void Jump()
    {
        isReadyToJump = true;
    }

    public void HaltJump()
    {
        isReadyToJump = false;
    }

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<AdvancedAnimator>();
    }

    void Update()
    {
        if (LevelManager.Instance.InGameAndRunning())
        {
            HandleMovement();
            HandleAnimation();
        }
        else
        {
            if(isNPC && xMovement != 0f)
            {
                savedXMovement = xMovement;
            }

            SetHorizontalMovement(0f);
            rb.velocity = Vector2.zero;
            HaltJump();
        }
    }

    void HandleMovement()
    {
        if (savedXMovement != 0f)
        {
            xMovement = savedXMovement;
            savedXMovement = 0f;
        }

        rb.velocity = new Vector2(xMovement * runSpeed, rb.velocity.y);

        if(isReadyToJump)
        {
            isReadyToJump = false;
            if (isOnGround)
            {
                OnJump();
            }
        }

        if (!isFalling && rb.velocity.y < 0f)
        {
            OnFall();
        }

        if (rb.velocity.y >= 0f)
        {
            isFalling = false;
        }

        if (!isGrounded && isOnGround)
        {
            OnLand();
        }

        if (hasJustJumped && !isOnGround)
        {
            hasJustJumped = false;
            isGrounded = false;
        }
    }

    void HandleAnimation()
    {
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        if (rb.velocity.x != 0f)
        {
            if (rb.velocity.x < 0f)
            {
                anim.SetFloat("Direction", -1f);
                anim.SetXScale(-1f);
            }
            if (rb.velocity.x > 0f)
            {
                anim.SetFloat("Direction", 1f);
                anim.SetXScale(1f);
            }

            if (isGrounded)
            {
                currentTimeBetweenSteps += Time.deltaTime;
                if (currentTimeBetweenSteps >= timeBetweenSteps)
                {
                    currentTimeBetweenSteps = 0f;
                    OnStep();
                }
            }
            else
            {
                currentTimeBetweenSteps = timeBetweenSteps / 2f;
            }
        }
        else
        {
            if (currentTimeBetweenSteps != timeBetweenSteps / 2f)
            {
                if (isGrounded)
                {
                    OnStep();
                }
                currentTimeBetweenSteps = timeBetweenSteps / 2f;
            }
        }
    }

    void OnJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        hasJustJumped = true;

        // Animation
        anim.SetTrigger("Jump");
    }

    void OnFall()
    {
        isFalling = true;

        // Animation
        anim.SetTrigger("Fall");
    }

    void OnLand()
    {
        isGrounded = true;

        // Animation
        anim.SetTrigger("Land");
    }

    void OnStep()
    {

    }
}
