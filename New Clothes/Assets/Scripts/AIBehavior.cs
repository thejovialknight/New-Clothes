using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior
{
    public string label = "Behaviour";
    public bool isActive = false;

    public virtual void Init()
    {

    }

    public virtual BehaviorNodeState Tick()
    {
        return BehaviorNodeState.Failure;
    }

    public virtual void InactiveTick()
    {

    }

    public virtual void OnEnd()
    {

    }

    public virtual void OnStart()
    {

    }
}
