using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clothing Item", menuName = "ScriptableObjects/Clothing Item")]
public class Item : ScriptableObject
{
    public string tag = "ClothingItem";
    public string label = "Clothing Item";
    public Slot slot;
    public float stealth = 1f;
    public Sprite pickupSprite;
    public RuntimeAnimatorController animator;
}