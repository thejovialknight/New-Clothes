using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    public ClothingInventory playerInventory;
    public float cooldown = 0f;
    public string currentText = "";
    public bool isOn = false;

    Text text;

    public float blinkLength = 0.25f;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = "";
    }

    void Start()
    {
        currentText = "";
    }

    void OnEnable()
    {
        LevelManager.onEndGame += OnEndGame;
        playerInventory.onAddItem += OnAddItem;
        playerInventory.onAddItemFinish += OnAddItemFinished;
    }

    void OnDisable()
    {
        LevelManager.onEndGame -= OnEndGame;
        playerInventory.onAddItem -= OnAddItem;
        playerInventory.onAddItemFinish -= OnAddItemFinished;
    }

    void Update()
    {
        // if (LevelManager.Instance.state == LevelState.PostGame)
        // {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0f)
            {
                isOn = !isOn;
                if (isOn)
                    text.text = currentText;
                else
                    text.text = "";

                cooldown = blinkLength;
            }
        // }
    }

    public void OnAddItem(ClothingItem item)
    {
        Debug.Log("It added");
        currentText = item.label.ToUpper() + " FOUND!";
    }

    public void OnAddItemFinished()
    {
        Debug.Log("It dun added");
        currentText = "";
    }

    public void OnEndGame(bool isWin)
    {
        if (isWin)
        {
            currentText = "YOU WIN!";
        }
        else
        {
            currentText = "YOU LOSE!";
        }
    }
}
