using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterEngine))]
public class Patrol : AIBehaviour
{
    public float timerAtFinal = 60f;

    public PatrolSettings settings;

    float cooldown;

    CharacterEngine engine;
    LineOfSight los;

    void Awake()
    {
        engine = GetComponent<CharacterEngine>();
        los = GetComponent<LineOfSight>();

        label = "Patrol";
    }

    public override void Tick()
    {
        settings.curMinIdleLength = CurrentLength(settings.minIdleLength, settings.finalMinIdleLength);
        settings.curMaxIdleLength = CurrentLength(settings.maxIdleLength, settings.finalMaxIdleLength);
        settings.curMinWalkLength = CurrentLength(settings.minWalkLength, settings.finalMinWalkLength);
        settings.curMaxWalkLength = CurrentLength(settings.maxWalkLength, settings.finalMaxWalkLength);

        if (LevelManager.Instance.InGameAndRunning())
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                if(engine.xMovement == 0f) {
                    cooldown = Random.Range(settings.curMinWalkLength, settings.curMaxWalkLength);
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
                    cooldown = Random.Range(settings.curMinIdleLength, settings.curMaxIdleLength);
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
