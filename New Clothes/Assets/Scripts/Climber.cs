using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public float climbSpeed = 1f;
    public float requiredDistanceToEntrance = 0.5f;

    public Stairs currentStairs;
    public float positionOnStairs;
    public float yMovement = 0f;
    public float xMovement = 0f;

    CharacterEngine engine;
    RadiusInteractor interactor;
    AdvancedAnimator anim;

    public void EnterStairs(Stairs stairs, bool isAtTop)
    {
        currentStairs = stairs;
        engine.enabled = false;
        CheckPosition(isAtTop);
        LevelManager.Instance.cam.isYOverride = true;
    }

    public void ExitStairs(Stairs stairs, bool isAtTop)
    {
        if(isAtTop)
        {
            transform.position = currentStairs.Top();
            LevelManager.Instance.playerFloorLevel = currentStairs.topRoom.floor;
        }
        else
        {
            transform.position = currentStairs.Bottom();
            LevelManager.Instance.playerFloorLevel = currentStairs.bottomRoom.floor;
        }

        engine.enabled = true;
        currentStairs = null;
        LevelManager.Instance.cam.isYOverride = false;
    }

    public void SetVerticalMovement(float movement)
    {
        yMovement = movement;
    }

    public void SetHorizontalMovement(float movement)
    {
        xMovement = movement;
    }

    void CheckPosition(bool isAtTop)
    {
        if (isAtTop)
        {
            positionOnStairs = 0.98f;
        }
        else
        {
            positionOnStairs = 0.02f;
        }
    }

    void HandleStairMovement()
    {
        anim.SetFloat("xVelocity", 0);
        if (yMovement != 0f)
        {
            positionOnStairs += yMovement * climbSpeed * Time.deltaTime;
            anim.SetFloat("xVelocity", Mathf.Abs(-yMovement));
            anim.SetFloat("Direction", -yMovement);
            anim.SetXScale(-yMovement);
        }
        else if(xMovement != 0f)
        {
            positionOnStairs -= xMovement * climbSpeed * Time.deltaTime;
            anim.SetFloat("xVelocity", Mathf.Abs(xMovement));
            anim.SetFloat("Direction", xMovement);
            anim.SetXScale(xMovement);
        }
        transform.position = Vector3.Lerp(currentStairs.Bottom(), currentStairs.Top(), positionOnStairs);
        LevelManager.Instance.playerFloorLevel = Mathf.Lerp(currentStairs.bottomRoom.floor, currentStairs.topRoom.floor, positionOnStairs);
        LevelManager.Instance.cam.yOverride = Mathf.Lerp(currentStairs.bottomCameraY, currentStairs.topCameraY, positionOnStairs);

        if(positionOnStairs < 0.5f)
        {
            LevelManager.Instance.playerLocation = currentStairs.bottomRoom;
        }
        else
        {
            LevelManager.Instance.playerLocation = currentStairs.topRoom;
        }

        if (positionOnStairs >= 1f)
        {
            transform.position = currentStairs.Top();
            ExitStairs(currentStairs, true);
        }
        else if (positionOnStairs <= 0f)
        {
            transform.position = currentStairs.Bottom();
            ExitStairs(currentStairs, false);
        }
    }

    void CheckForStairs()
    {
        StairEntrance entrance = interactor.currentStairEntrance;
        if(entrance != null && Vector3.Distance(transform.position, entrance.transform.position) < requiredDistanceToEntrance)
        {
            if(entrance.isTop && yMovement < 0f)
            {
                entrance.Interact(transform);
            }
            else if (!entrance.isTop && yMovement > 0f)
            {
                entrance.Interact(transform);
            }
        }
    }

    void Awake()
    {
        engine = GetComponent<CharacterEngine>();
        interactor = GetComponent<RadiusInteractor>();
        anim = GetComponent<AdvancedAnimator>();
    }

    void Update()
    {
        if (currentStairs != null)
        {
            HandleStairMovement();
        }
        else if (yMovement != 0f)
        {
            CheckForStairs();
        }
    }
}
