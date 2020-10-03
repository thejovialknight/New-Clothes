using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
    public string sceneName;

    public void Replay()
    {
        GameManager.Instance.EnterGame(sceneName);
    }
}
