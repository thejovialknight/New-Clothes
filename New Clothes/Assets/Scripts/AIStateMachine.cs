using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherit with all the state machines
public abstract class AIStateMachine : MonoBehaviour
{
    public int currentState = 0;
    public List<AIState> states = new List<AIState>();

    void Start()
    {
        AddStates();

        states[currentState].EnterState();
    }

    void Update()
    {
        states[currentState].TickState();
    }

    public abstract void AddStates();

    public void SetState(string stateName)
    {
        for(int i = 0; i < states.Count; i++)
        {
            if(stateName == states[i].Name)
            {
                states[currentState].ExitState();
                currentState = i;
                states[currentState].EnterState();
            }
        }
    }
}
