using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public string label = "Behaviour";
    public bool isActive = false;
    public AIController controller;

    public virtual void Init(AIController controller)
    {
        this.controller = controller;
    }

    public virtual void Tick()
    {

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
