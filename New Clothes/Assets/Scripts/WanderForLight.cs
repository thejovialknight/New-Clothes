using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderForLight : AIBehaviour
{
    public float wanderSpeed = 1f;

    public float previousRunSpeed;
    public PatrolSettings previousPatrolSettings;

    Patrol patrol;
    Stealth stealth;
    CharacterEngine engine;
    RadiusInteractor interactor;

    void Awake()
    {
        patrol = GetComponent<Patrol>();
        engine = GetComponent<CharacterEngine>();
        interactor = GetComponent<RadiusInteractor>();
        stealth = GetComponent<Stealth>();

        label = "WanderForLight";
    }

    public override void OnStart()
    {
        base.OnStart();

        if (engine.runSpeed != wanderSpeed)
        {
            previousRunSpeed = engine.runSpeed;
            engine.runSpeed = wanderSpeed;
        }

        if(!patrol.settings.isTemp)
        {
            previousPatrolSettings = patrol.settings;
            patrol.settings = new PatrolSettings(true, 0f, 0f, 0.5f, 3f, 0f, 0f, 2f, 4f);
        }
    }

    public override void OnEnd()
    {
        base.OnEnd();

        engine.runSpeed = previousRunSpeed;
        patrol.settings = previousPatrolSettings;
    }

    public override void Tick()
    {
        if(interactor.targetTransform != null && interactor.targetTransform.CompareTag("Light"))
        {
            if(interactor.Interact())
            {
                controller.SetBehaviourOn(label, false);
            }
        }

        if(stealth.LightLevel() > 0f)
        {
            controller.SetBehaviourOn(label, false);
        }
    }

    public override void InactiveTick()
    {
        if (stealth.LightLevel() <= 0f)
        {
            controller.SetBehaviourOn(label, true);
        }
    }
}
