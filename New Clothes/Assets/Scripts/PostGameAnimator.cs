using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameAnimator : MonoBehaviour
{
    AdvancedAnimator animator;

    public bool isEnemy;

    void Awake()
    {
        animator = GetComponent<AdvancedAnimator>();
    }

    void OnEnable()
    {
        LevelManager.onEndGame += OnEndGame;
    }

    void OnDisable()
    {
        LevelManager.onEndGame -= OnEndGame;
    }

    public void OnEndGame(bool isWin)
    {
        if (isEnemy)
        {
            isWin = !isWin;
        }

        if (isWin)
        {
            animator.SetTrigger("Celebrate");
        }
        else
        {
            animator.SetTrigger("Lose");
        }
    }
}
