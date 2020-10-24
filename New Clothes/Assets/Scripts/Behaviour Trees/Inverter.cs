using UnityEngine; 
using System.Collections; 
 
[CreateAssetMenu(fileName = "Inverter", menuName = "Behavior Nodes/Inverter", order = 4)]
public class Inverter : BehaviorNode { 
    /* Child node to evaluate */ 
    public BehaviorNode node; 
 
    /* The constructor requires the child node that this inverter decorator 
     * wraps*/ 
    public Inverter(BehaviorNode node) { 
        this.node = node; 
    } 
 
    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */ 
    public override BehaviorNodeState Evaluate() { 
        switch (node.Evaluate()) { 
            case BehaviorNodeState.Failure: 
                nodeState = BehaviorNodeState.Success; 
                return nodeState; 
            case BehaviorNodeState.Success: 
                nodeState = BehaviorNodeState.Failure; 
                return nodeState; 
            case BehaviorNodeState.Running: 
                nodeState = BehaviorNodeState.Running; 
                return nodeState; 
        } 
        nodeState = BehaviorNodeState.Success; 
        return nodeState; 
    } 
}