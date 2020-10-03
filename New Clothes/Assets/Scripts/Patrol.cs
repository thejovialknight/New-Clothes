using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterEngine))]
public class Patrol : MonoBehaviour
{
    public float timerAtFinal = 60f;

    public float minIdleLength = 2f;
    public float maxIdleLength = 4f;
    public float minWalkLength = 0.5f;
    public float maxWalkLength = 1f;

    public float finalMinIdleLength = 0.5f;
    public float finalMaxIdleLength = 1f;
    public float finalMinWalkLength = 1f;
    public float finalMaxWalkLength = 3f;

    public float curMinIdleLength;
    public float curMaxIdleLength;
    public float curMinWalkLength;
    public float curMaxWalkLength;

    float cooldown;

    CharacterEngine engine;
    LineOfSight los;

    void Awake()
    {
        engine = GetComponent<CharacterEngine>();
        los = GetComponent<LineOfSight>();
    }

    void Update()
    {
        curMinIdleLength = CurrentLength(minIdleLength, finalMinIdleLength);
        curMaxIdleLength = CurrentLength(maxIdleLength, finalMaxIdleLength);
        curMinWalkLength = CurrentLength(minWalkLength, finalMinWalkLength);
        curMaxWalkLength = CurrentLength(maxWalkLength, finalMaxWalkLength);

        if (LevelManager.Instance.InGameAndRunning())
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                if(engine.xMovement == 0f) {
                    cooldown = Random.Range(curMinWalkLength, curMaxWalkLength);
                    int decision = Random.Range(0, 2);
                    switch (decision)
                    {
                        case 0:
                            engine.SetHorizontalMovement(1f);
                            break;
                        case 1:
                            engine.SetHorizontalMovement(-1f);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    cooldown = Random.Range(curMinIdleLength, curMaxIdleLength);
                    engine.SetHorizontalMovement(0f);
                }

                if (engine.xMovement != 0f)
                {
                    los.target = new Vector2(transform.position.x + engine.xMovement * 1000f, transform.position.y);
                }
                // probably eventually just automate LOS to follow velocity in a separate script.
            }

            if (los.PlayerInSight())
            {
                LevelManager.Instance.EndGame(false);
            }
        }
    }

    float CurrentLength(float startingValue, float finalValue)
    {
        return Mathf.Lerp(startingValue, finalValue, LevelManager.Instance.timeElapsed / timerAtFinal);
    }
}
