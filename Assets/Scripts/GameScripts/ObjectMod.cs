using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMod : MonoBehaviour {


    public MasterReferences master;

    public GameObject eye, crosshair;

    public void ModSettings()
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
            case Modifiers.Modifier.WATCHINGYOU:
                Mod_Eye();
                break;
            case Modifiers.Modifier.OUTTOGETYA:
                Mod_Crosshair();
                break;
        }
    }

    void Mod_Eye ()
    {
        eye.active = true;
    }

    void Mod_Crosshair()
    {
        crosshair.active = true;
    }
}
