using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "Behavior Nodes/Sequence", order = 3)]
public class Sequence : BehaviorNode
{
    /** Children nodes that belong to this sequence */ 
    public List<BehaviorNode> nodes = new List<BehaviorNode>(); 
 
     /** Must provide an initial set of children nodes to work */ 
    public Sequence(List<BehaviorNode> nodes) { 
        this.nodes = nodes; 
    } 
 
    /* If any child node returns a failure, the entire node fails. Whence all  
     * nodes return a success, the node reports a success. */ 
    public override BehaviorNodeState Evaluate() { 
        bool isAnyChildRunning = false;

        foreach (BehaviorNode node in nodes) { 
            switch (node.Evaluate()) { 
                case BehaviorNodeState.Failure: 
                    nodeState = BehaviorNodeState.Failure; 
                    return nodeState;
                case BehaviorNodeState.Success: 
                    continue;
                case BehaviorNodeState.Running: 
                    isAnyChildRunning = true;
                    continue;
                default: 
                    nodeState = BehaviorNodeState.Success; 
                    return nodeState; 
            } 
        } 
        nodeState = isAnyChildRunning ? BehaviorNodeState.Running : BehaviorNodeState.Success; 
        return nodeState; 
    } 
}
