using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnPostGame : MonoBehaviour
{
    public List<GameObject> menuItems = new List<GameObject>();

    void OnEnable()
    {
        LevelManager.onEndGame += OnEndGame;
    }

    void OnDisable()
    {
        LevelManager.onEndGame -= OnEndGame;
    }

    void Start()
    {
        foreach (GameObject button in menuItems)
        {
            button.SetActive(false);
        }
    }

    void OnEndGame(bool isWin)
    {
        foreach (GameObject button in menuItems)
        {
            button.SetActive(true);
        }
    }
}
