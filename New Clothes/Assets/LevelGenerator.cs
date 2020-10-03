using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Generation Settings")]
    public int numberOfShirts = 2;
    public int numberOfPants = 2;

    public List<int> filledContainerIDs = new List<int>();
    List<ClothingContainer> containers = new List<ClothingContainer>();

    void OnEnable()
    {
        LevelManager.onPostInit += OnLoad;
    }

    void OnDisable()
    {
        LevelManager.onPostInit -= OnLoad;
    }

    void OnLoad()
    {
        containers = LevelManager.Instance.containers;

        if (containers.Count < numberOfPants + numberOfShirts)
        {
            Debug.LogError("Not enough containers! Needs " + (numberOfShirts + numberOfPants) + "and only has " + containers.Count + "!");
            Debug.Break();
            return;
        }

        PopulateBySlot(ClothingSlot.Torso, numberOfShirts);
        PopulateBySlot(ClothingSlot.Legs, numberOfPants);
    }

    void PopulateBySlot(ClothingSlot slot, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int containerIDToFill = -1;
            bool foundEmptyContainer = false;
            int iterations = 0;
            while (foundEmptyContainer == false)
            {
                containerIDToFill = Random.Range(0, containers.Count);
                foundEmptyContainer = true;
                foreach (int containerID in filledContainerIDs)
                {
                    if (containerID == containerIDToFill)
                    {
                        foundEmptyContainer = false;
                    }
                }

                iterations++;
                if(iterations > 100)
                {
                    Debug.LogError("Dear me, too many tries!");
                    Debug.Break();
                    return;
                }
            }

            containers[containerIDToFill].contained = GetRandomItemOfSlot(slot);
            filledContainerIDs.Add(containerIDToFill);
            Debug.Log("Item added: " + containers[containerIDToFill].name);
        }
    }

    ClothingItem GetRandomItemOfSlot(ClothingSlot slot)
    {
        ClothingItem[] itemPool = ClothingDatabase.Instance.ItemsOfSlot(slot);
        return itemPool[Random.Range(0, itemPool.Length - 1)];
    }
}
