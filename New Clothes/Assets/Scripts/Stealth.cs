using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : MonoBehaviour
{
    public float baseStealth = 0f;
    public List<StealthModifier> modifiers = new List<StealthModifier>();
    public float unlitStealthMod = 10f;

    [Header("Info")]
    public float rating;

    // between 0 and 1
    public float localLightLevel;

    // higher is better? 
    public float Rating()
    {
        float totalMods = 0f;
        foreach (StealthModifier mod in modifiers)
        {
            totalMods += mod.value;
        }
        totalMods += LightModifier();

        return totalMods;
    }

    // Between 0 and 1
    public void SetLocalLight(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        localLightLevel = value;
    }

    // 0 is unlit, 1 is fully lit
    public float LightLevel()
    {
        if (LevelManager.Instance.playerLocation.isLightOn)
        {
            return 1f;
        }

        return localLightLevel;
    }

    public float LightModifier()
    {

        // TODO: ONLY WORKS FOR PLAYER!! ALSO IMPLEMENT LIGHTLEVEL()
        if (LevelManager.Instance.playerLocation.isLightOn)
        {
            return 0f;
        }

        float lightMod = unlitStealthMod;

        lightMod += Mathf.Lerp(unlitStealthMod, 0f, localLightLevel);

        return Mathf.Lerp(unlitStealthMod, 0f, localLightLevel);
    }

    public void SetModifier(string tag, float value)
    {
        foreach (StealthModifier mod in modifiers)
        {
            if (mod.tag == tag)
            {
                mod.value = value;
                return;
            }
        }

        modifiers.Add(new StealthModifier(tag, value));
    }

    void Update()
    {
        rating = Rating();
    }
}