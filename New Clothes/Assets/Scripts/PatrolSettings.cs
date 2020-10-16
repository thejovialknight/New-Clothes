using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatrolSettings
{
    public bool isTemp = false;

    public float minIdleLength = 2f;
    public float maxIdleLength = 4f;
    public float minWalkLength = 0.5f;
    public float maxWalkLength = 1f;

    public float finalMinIdleLength = 0.5f;
    public float finalMaxIdleLength = 1f;
    public float finalMinWalkLength = 1f;
    public float finalMaxWalkLength = 3f;

    public float curMinIdleLength;
    public float curMaxIdleLength;
    public float curMinWalkLength;
    public float curMaxWalkLength;

    public PatrolSettings(bool isTemp, float minIdle, float maxIdle, float minWalk, float maxWalk, float minIdleF, float maxIdleF, float minWalkF, float maxWalkF)
    {
        this.isTemp = isTemp;
        this.minIdleLength = minIdle;
        this.maxIdleLength = maxIdle;
        this.minWalkLength = minWalk;
        this.maxWalkLength = maxWalk;
        this.finalMinIdleLength = minIdleF;
        this.finalMaxIdleLength = maxIdleF;
        this.finalMinWalkLength = minWalkF;
        this.finalMaxWalkLength = maxWalkF;
    }
}
