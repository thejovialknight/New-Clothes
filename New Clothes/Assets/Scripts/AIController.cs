using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Selector rootNode;

    public AIStateMachine machine;

    // TODO: AUTO POPULATE THIS JUST LIKE USED TO DO INTERACTORS WHEN THAT WAS A LIST
    public List<AIBehavior> behaviours = new List<AIBehavior>();

    public Stealth stealth;

    public void SetBehaviourOn(string label, bool isOn)
    {
        foreach(AIBehavior behaviour in behaviours)
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
        behaviours = GetComponents<AIBehavior>().ToList();

        foreach (AIBehavior behaviour in behaviours)
        {
            // behaviour.Init(this);
        }

        stealth = GetComponent<Stealth>();
    }

    void Update()
    {
        rootNode.Evaluate();

        /*
        foreach (AIBehaviour behaviour in behaviours)
        {
            behaviour.InactiveTick();

            if (behaviour.isActive) {
                behaviour.Tick();
            }
        }
        */
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
