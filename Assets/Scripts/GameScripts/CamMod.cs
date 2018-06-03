using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CamMod : MonoBehaviour {

    public MasterReferences master;
    public Screenshake screenshake;
    public PostProcessingBehaviour ppProfile;
    public PostProcessingProfile cinematicProfile;
    public Assets.Pixelation.Scripts.Pixelation pixelateEffect;
    public UnityStandardAssets.ImageEffects.Twirl alienEffect;
    public GameObject alienPlane;
    public UnityStandardAssets.ImageEffects.EdgeDetection cartoonEffect;
    public GameObject cartoonPlane;
    public UnityStandardAssets.ImageEffects.VignetteAndChromaticAberration trippyEffect;
    public GameObject nightPlane;
    public UnityStandardAssets.ImageEffects.ContrastEnhance nightEffect;
    public UnityStandardAssets.ImageEffects.ColorCorrectionCurves seriousEffect;
    public UnityStandardAssets.ImageEffects.NoiseAndScratches seriousEffect2;
    public UnityStandardAssets.ImageEffects.Fisheye wideEffect;
    public UnityStandardAssets.ImageEffects.ScreenOverlay cinematicEffect;
    public GameObject fpsCam;
    public GameObject errorParticles;
    public MirrorFlipCamera flipCam;
    public GameObject cinematicRatio, calenderUI;



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
            case Modifiers.Modifier.UNSTABLE:
                Mod_Unstable();
                break;
            case Modifiers.Modifier.WIDE:
                Mod_Wide();
                break;
            case Modifiers.Modifier.INCEPTION:
                Mod_Inception();
                break;
            case Modifiers.Modifier.NULLREF:
                Mod_Error();
                break;
            case Modifiers.Modifier.LEFTHANDED:
                Mod_LeftHand();
                break;
            case Modifiers.Modifier.CINEMATIC:
                Mod_Cinematic();
                break;
        }
    }

    void Mod_Cinematic()
    {
        ppProfile.profile = cinematicProfile;
        cinematicEffect.enabled = true;
        cinematicRatio.active = true;
        calenderUI.active = false;
    }

    void Mod_LeftHand()
    {
        flipCam.flipHorizontal = true;
    }


    void Mod_Error()
    {
        errorParticles.active = true;
    }

    void Mod_Inception()
    {
        fpsCam.active = true;
    }

    void Mod_Unstable ()
    {
        screenshake.unstable = true;

    }

    void Mod_Wide()
    {
        wideEffect.enabled = true;
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
        Time.timeScale = 1.5f;
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
        seriousEffect2.enabled = true;
    }



}
