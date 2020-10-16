using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public AIStateMachine machine;

    // TODO: AUTO POPULATE THIS JUST LIKE USED TO DO INTERACTORS WHEN THAT WAS A LIST
    public List<AIBehaviour> behaviours = new List<AIBehaviour>();

    public Stealth stealth;

    public void SetBehaviourOn(string label, bool isOn)
    {
        foreach(AIBehaviour behaviour in behaviours)
        {
            if(behaviour.label == label)
            {
                if(isOn)
                {
                    behaviour.OnStart();
                    behaviour.isActive = true;
                }
                else
                {
                    behaviour.OnEnd();
                    behaviour.isActive = false;
                }

                return;
            }
        }
    }

    void Awake()
    {
        behaviours = GetComponents<AIBehaviour>().ToList();

        foreach (AIBehaviour behaviour in behaviours)
        {
            behaviour.Init(this);
        }

        stealth = GetComponent<Stealth>();
    }

    void Update()
    {
        foreach (AIBehaviour behaviour in behaviours)
        {
            behaviour.InactiveTick();

            if (behaviour.isActive) {
                behaviour.Tick();
            }
        }
    }

    void OnEnable()
    {
        LevelManager.onInit += OnLoad;
    }

    void OnDisable()
    {
        LevelManager.onInit -= OnLoad;
    }

    void OnLoad()
    {
        LevelManager.Instance.RegisterAIController(this);
    }
}
