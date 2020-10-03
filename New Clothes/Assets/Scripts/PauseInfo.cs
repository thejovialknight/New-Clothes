using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PauseInfo
{
    public bool isPaused;
    public bool isInputPaused;

    public PauseInfo(bool isPaused)
    {
        this.isPaused = isPaused;
        this.isInputPaused = isPaused;
    }

    public PauseInfo(bool isPaused, bool isInputPaused)
    {
        this.isPaused = isPaused;
        this.isInputPaused = isInputPaused;
    }
}
