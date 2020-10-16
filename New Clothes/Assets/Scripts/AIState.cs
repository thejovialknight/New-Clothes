using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    public AIStateMachine machine;
    public string label = "State";

    public string Name => label;

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void TickState();
}
