using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public MoveMotor motor;

    public Renderer renderer;

    public Texture nullAccessory;
    public List<Texture> accessories;

    public Renderer accessoryHolder;

    private void Start()
    {
        accessories = new List<Texture>(Resources.LoadAll<Texture>("accessories"));
        foreach (Modifiers.Modifier mod in motor.master.modifiers.mods)
        {
            ModSettings_Start(mod);
        }

        Texture getAccessory = LoadAccessory();
        if (getAccessory != null)
        {
            SetAccessory(getAccessory);
        }
    }

    Texture LoadAccessory()
    {
        int id = PlayerPrefs.HasKey("Accessory") ? PlayerPrefs.GetInt("Accessory") : -1;
        if (id == -1) return nullAccessory;

        return accessories[id];
    }

    public void GetGift ()
    {
        Texture accessory = GetRandomGift();
        SetAccessory(accessory);
    }



    Texture GetRandomGift ()
    {
        int accessoryID = GetRandomGiftID();
        PlayerPrefs.SetInt("Accessory", accessoryID);
        return accessories[accessoryID];
    }


    int GetRandomGiftID()
    {
        int random = Random.Range(0, accessories.Count);
        if (PlayerPrefs.HasKey("Accessory"))
        {
            //ensures it won't choose the same item
            return PlayerPrefs.GetInt("Accessory") != random ? random : GetRandomGiftID();
        }
        else
        {
            return Random.Range(0, accessories.Count);
        }
    }

    void SetAccessory (Texture accessory)
    {
        accessoryHolder.material.mainTexture = accessory;
    }


    void ModSettings_Start(Modifiers.Modifier mod)
    {
        switch (mod)
        { 
            case Modifiers.Modifier.ANGRY:
                Mod_Angry();
                break;
        }
    }


    void Mod_Angry()
    {
        renderer.material.color = Color.red;
    }




}
