using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : AIState
{
    CharacterEngine engine;

    public WanderState(AIStateMachine machine)
    {
        label = "Wander";
        this.machine = machine;

        engine = machine.GetComponent<CharacterEngine>();
    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void TickState()
    {

    }
}
