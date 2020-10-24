using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Selector", menuName = "Behavior Nodes/Selector", order = 2)]
public class Selector : BehaviorNode
{
    /** The child nodes for this selector */ 
    public List<BehaviorNode> nodes = new List<BehaviorNode>(); 
 
    /** The constructor requires a list of child nodes to be  
     * passed in*/ 
    public Selector(List<BehaviorNode> nodes) { 
        this.nodes = nodes; 
    } 
 
    /* If any of the children reports a success, the selector will 
     * immediately report a success upwards. If all children fail, 
     * it will report a failure instead.*/ 
    public override BehaviorNodeState Evaluate() { 
        foreach (BehaviorNode node in nodes) { 
            switch (node.Evaluate()) { 
                case BehaviorNodeState.Failure: 
                    continue; 
                case BehaviorNodeState.Success: 
                    nodeState = BehaviorNodeState.Success; 
                    return nodeState; 
                case BehaviorNodeState.Running: 
                    nodeState = BehaviorNodeState.Running; 
                    return nodeState; 
                default: 
                    continue; 
            } 
        } 
        nodeState = BehaviorNodeState.Failure; 
        return nodeState; 
    } 
}
