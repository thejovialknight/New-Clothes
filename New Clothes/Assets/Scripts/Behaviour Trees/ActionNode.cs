using System; 
using UnityEngine; 
using System.Collections; 
 
[CreateAssetMenu(fileName = "ActionNode", menuName = "Behavior Nodes/Action Node", order = 1)]
public class ActionNode : BehaviorNode { 
    // the behavior
    public AIBehavior behavior;
 
    /* Because this node contains no logic itself, 
     * the logic must be passed in in the form of  
     * a delegate. As the signature states, the action 
     * needs to return a NodeStates enum */ 
    public ActionNode(AIBehavior behavior) { 
        this.behavior = behavior; 
    } 
 
    /* Evaluates the node using the passed in delegate and  
     * reports the resulting state as appropriate */ 
    public override BehaviorNodeState Evaluate() { 
        switch (behavior.Tick()) { 
            case BehaviorNodeState.Success: 
                nodeState = BehaviorNodeState.Success; 
                return nodeState; 
            case BehaviorNodeState.Failure: 
                nodeState = BehaviorNodeState.Failure; 
                return nodeState; 
            case BehaviorNodeState.Running: 
                nodeState = BehaviorNodeState.Running; 
                return nodeState; 
            default: 
                nodeState = BehaviorNodeState.Failure; 
                return nodeState; 
        } 
    } 
}