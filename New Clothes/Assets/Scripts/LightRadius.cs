using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRadius : MonoBehaviour
{
    public float innerRadius = 1.5f;
    public float outerRadius = 2f;
    public float checkRadius = 3f;

    public List<Stealth> nearby = new List<Stealth>();

    void LateUpdate()
    {
        UpdateNearbyAgents();
        SetAgentLights();
    }

    void OnDisable()
    {
        foreach (Stealth stealth in nearby)
        {
            stealth.SetLocalLight(0f);
        }
    }

    void UpdateNearbyAgents()
    {
        nearby.Clear();
        CheckAgent(LevelManager.Instance.playerStealth);

        /*
        foreach (AIController ai in LevelManager.Instance.AIs)
        {
            CheckAgent(ai.stealth);
        }
        */
    }

    void CheckAgent(Stealth stealth)
    {
        if (Vector3.Distance(transform.position, stealth.transform.position) < checkRadius)
        {
            nearby.Add(stealth);
        }
    }

    void SetAgentLights()
    {
        foreach(Stealth stealth in nearby)
        {
            SetLightLevel(stealth);
        }
    }

    void SetLightLevel(Stealth stealth)
    {
        if (Vector3.Distance(transform.position, stealth.transform.position) < outerRadius)
        {
            stealth.SetLocalLight(0.5f);
            if(Vector3.Distance(transform.position, stealth.transform.position) < innerRadius)
            {
                stealth.SetLocalLight(1f);
            }
        }
        else
        {
            stealth.SetLocalLight(0f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, outerRadius);
    }
}
