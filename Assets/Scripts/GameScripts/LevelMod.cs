using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMod : MonoBehaviour {

    public MasterReferences master;


    bool isSpinning;
    float spinAngle = 0;

    public void RunMod()
    {
            foreach (Modifiers.Modifier mod in master.modifiers.mods)
            {
                ModSettings_Start(mod);
            }
        
    }


    void ModSettings_Start(Modifiers.Modifier mod)
    {
        switch (mod)
        {
            case Modifiers.Modifier.SPINNING:
                Mod_Spinning();
                break;
            case Modifiers.Modifier.VOID:
                Mod_Void();
                break;
            case Modifiers.Modifier.PRESENTLESS:
                Mod_Presentless();
                break;
            case Modifiers.Modifier.GROUNDLESS:
                Mod_Groundless();
                break;
            case Modifiers.Modifier.LOVELY:
                Mod_Lovely();
                break;
        }
    }

    void Mod_Lovely()
    {
        Renderer[] renders = transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renders)
        {
            Material[] mats = render.materials;

            foreach (Material mat in mats)
            {
                bool chance = Random.Range(0, 100) < 15; //15% red

                if (chance)
                {
                    mat.color = new Color(1, 0, 0);
                }
            }
        }
    }

    void Mod_Groundless ()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Base"))
            {
                child.gameObject.active = false;
            }
        }
    }


    void Mod_Spinning()
    {
        isSpinning = true;
        spinAngle = Random.Range(0.06f, 0.15f);
        spinAngle *= Random.Range(0, 2) == 1 ? -1 : 1;
    }

    void Mod_Void()
    {
        Renderer[] renders = transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renders)
        {
            Material[] mats = render.materials;

            foreach (Material mat in mats)
            {
                bool chance = Random.Range(0, 100) < 25; //25% black

                if (chance)
                {
                    mat.color = new Color(0, 0, 0);
                }
            }
        }
    }

    void Mod_Presentless ()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Present"))
            {
                child.gameObject.active = false;
            }
        }
    }

    private void Update()
    {
        if (isSpinning)
        {
            transform.Rotate(new Vector3(0, 0, spinAngle));
        }
    }
}


//0.1-0.3
