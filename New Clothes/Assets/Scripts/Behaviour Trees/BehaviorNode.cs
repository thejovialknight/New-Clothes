using UnityEngine; 
using System.Collections; 
 
public abstract class BehaviorNode : ScriptableObject { 
 
    /* Delegate that returns the state of the node.*/ 
    public delegate BehaviorNodeState NodeReturn(); 
 
    /* The current state of the node */ 
    public BehaviorNodeState nodeState; 
 
    /* The constructor for the node */ 
    public BehaviorNode() {} 
 
    /* Implementing classes use this method to evaluate the desired set of conditions */ 
    public abstract BehaviorNodeState Evaluate(); 
 
}