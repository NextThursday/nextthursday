using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMod : MonoBehaviour {

    public MasterReferences master;
    public Assets.Pixelation.Scripts.Pixelation pixelateEffect;
    public UnityStandardAssets.ImageEffects.Twirl alienEffect;
    public GameObject alienPlane;
    public UnityStandardAssets.ImageEffects.EdgeDetection cartoonEffect;
    public GameObject cartoonPlane;
    public UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration trippyEffect;
    public GameObject nightPlane;
    public UnityStandardAssets.ImageEffects.ContrastEnhance nightEffect;
    public UnityStandardAssets.ImageEffects.ColorCorrectionCurves seriousEffect;



    public void ModSettings ()
    {
        foreach (Modifiers.Modifier mod in master.modifiers.mods)
        {
            ModSettings_Check(mod);
        }
    }

    void ModSettings_Check(Modifiers.Modifier mod)
    {
        switch (mod)
        {
            case Modifiers.Modifier.FROM_ANOTHER_TIME:
                Mod_Retro();
                break;
            case Modifiers.Modifier.RED:
                Mod_Alien();
                break;
            case Modifiers.Modifier.HOT:
                Mod_Cartoon();
                break;
            case Modifiers.Modifier.FASTER:
                Mod_Faster();
                break;
            case Modifiers.Modifier.TRIPPY:
                Mod_Trippy();
                break;
            case Modifiers.Modifier.WITHOUT_A_SUN:
                Mod_Night();
                break;
            case Modifiers.Modifier.GRUESOME:
                Mod_Serious();
                break;
        }
    }

    void Mod_Retro()
    {
        pixelateEffect.enabled = true;
    }

    void Mod_Alien()
    {
        alienPlane.active = true;
        alienEffect.enabled = true;
    }

    void Mod_Cartoon()
    {
        cartoonPlane.active = true;
        cartoonEffect.enabled = true;
    }

    void Mod_Faster()
    {
        Time.timeScale = 2;
    }


    void Mod_Trippy()
    {
        trippyEffect.enabled = true;
    }

    void Mod_Night ()
    {
        nightPlane.active = true;
        nightEffect.enabled = true;
    }

    void Mod_Serious()
    {
        seriousEffect.enabled = true;
    }



}
