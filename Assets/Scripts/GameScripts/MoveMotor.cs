﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMotor : MonoBehaviour {

    public MasterReferences master;



    [Header("REFERENCES")]
    public Rigidbody rigid;
    public Collider collider;
    public TargetHandler targetHandler;
    public ParticleSystem rushParticles;



    [Header("CONTROLS")]

    public bool isPlayer;

    [Tooltip("speed to move player")]
    public float forwardInitSpeed = 100f;
    [HideInInspector] public float forwardSpeed;
    [HideInInspector] public float forwardSpeedAdd1; //a space to enter values to add to forward speed

    [Tooltip("how much the target distance influences player speed (higher = less influence)")]
    public float targetDistanceSpeed = 1;

    [Tooltip("limits how close the player can get to the target")]
    public float targetDistanceThreshold = 0.6f;

    [Range(0,1)] [Tooltip("how slow the player gets when they approach the target (lower = slower)")]
    public float targetDistanceThresholdDamping = 0.1f;

    [Tooltip("x = min, y = max, it'll select a random speed between those two")]
    public Vector2 turnSpeedRange;

    [Tooltip("how long it takes to fire up the engines after it's off")]
    public float turnOnDelay;

    Vector3 target;
    [HideInInspector] public float turnSpeed;
    float turnSpeedInit;
    public float turnSpeedLineFormation;


    public bool active = false;
    public bool allowDeath = true;

    public float explodeRadius;

    bool waterState, teleportalState;
    float randomSeed;
    //1 = normal size, 2 = 2x size


    int hitState = 0;




    [Header("Drift")]
    public bool drift;
    bool allowDrift;
    public float driftInstability;
    public float driftStrength;


    [Header("Mod Effects")]
    bool modUpdateAllow;
    public PhysicMaterial bouncyPhysics;
    bool reverseMode;
    Vector3 initScale;

    public GameObject PointLoseParticle;


    public void On ()
    {

        StopAllCoroutines();
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn ()
    {
        yield return new WaitForSeconds(turnOnDelay);
        active = true;
    }








    private void Start()
    {
        randomSeed = Random.Range(0, 1000);
        turnSpeed = Random.Range(turnSpeedRange.x, turnSpeedRange.y);
        turnSpeedInit = turnSpeed;
        forwardSpeed = forwardInitSpeed;



        foreach (Modifiers.Modifier mod in master.modifiers.mods)
        {
            ModSettings_Start(mod);
        }


        initScale = transform.localScale;
        modUpdateAllow = true;

        if (master.controls.isTutorial)
        {
            forwardInitSpeed *= 0.5f;
            forwardSpeed *= 0.5f;
        }

        rigid.AddForce(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0));
    }

    public void Incinerate ()
    {
        GameObject pointObject = Instantiate(PointLoseParticle, transform);
        pointObject.transform.parent = pointObject.transform.parent.parent;
        pointObject.transform.localEulerAngles = Vector3.zero;

        DieAlly();
    }






    void ModSettings_Start(Modifiers.Modifier mod)
    {
        switch (mod)
        {
            case Modifiers.Modifier.SLIPPERY:
                Mod_Slippery();
                break;


        //    case Modifiers.Modifier.BOUNCY:
         //       Mod_Bouncy();
          //      break;

            case Modifiers.Modifier.ANGRY:
                Mod_Angry();
                break;

            case Modifiers.Modifier.BIGGER:
                Mod_Bigger();
                break;

            case Modifiers.Modifier.SMALLER:
                Mod_Smaller();
                break;

            case Modifiers.Modifier.FASTER:
                Mod_Faster();
                break;
        }
    }


    void Mod_Angry()
    {
        drift = true;
    }


    void Mod_Bigger()
    {
        transform.localScale *= Random.Range(0.3f, 0.6f);
        if (isPlayer)
        {

            forwardInitSpeed *= 2f;
            forwardSpeed *= 2f;
            rigid.mass *= 6f;
            rigid.drag *= 0.5f;
            driftStrength *= 0.3f;
        } else
        {
            rigid.drag *= 2f;

        }
    }


    void Mod_Smaller()
    {
        transform.localScale *= Random.Range(1.5f, 1.7f);
    }

    void Mod_Bouncy()
    {
        collider.sharedMaterial = bouncyPhysics;
        rigid.angularDrag *= 30f;
    }

    void Mod_Slippery()
    {
        rigid.drag *= 0.2f;
        rigid.angularDrag *= 0.5f;
        forwardInitSpeed *= 0.1f;
        forwardSpeed *= 0.1f;
        turnSpeedInit *= 0.6f;
        turnSpeed *= 0.6f;
    }

    void Mod_Faster ()
    {
        forwardSpeed *= 1.5f;
        forwardInitSpeed *= 1.5f;
    }


    public void isInWater (bool state)
    {
        waterState = state;
    }

    public void isInTeleportal (bool state)
    {
        teleportalState = state;
    }


    void Update () {
        
        if (active)
        {
            

            target = targetHandler.GetTarget();


            LookAtTarget();
            
                if (GetTargetDistance() > targetDistanceThreshold)
                {

                    Vector3 force = transform.right * forwardSpeed * (GetTargetDistance() + targetDistanceSpeed) * GetWaterSlowdown() * GetTeleportalSpeed();
                    if (reverseMode)
                    {
                        force *= -1;
                    }
                    rigid.AddForce(force);

                }
                else
                {
                    rigid.velocity *= targetDistanceThresholdDamping;
                }
            TargetHandlerMechanic();



            if (drift && allowDrift) Drift();
        }




        if (modUpdateAllow)
        {
            foreach (Modifiers.Modifier mod in master.modifiers.mods)
            {
                ModSettings_Update(mod);
            }
        }
    }


    void Drift()
    {
        Vector2 randomDriftSeed = new Vector2(randomSeed * 3, randomSeed * 8);

        rigid.AddForce(new Vector2(  (PerlinValue(Time.time + randomDriftSeed.x, driftInstability) + 0.07f) * driftStrength * GetWaterSlowdown(),
            (PerlinValue(Time.time + randomDriftSeed.y, driftInstability) + 0.07f) * driftStrength * GetWaterSlowdown()  ));
    }



    float GetWaterSlowdown()
    {
        float waterSlowdown = 1;
        if (waterState)
        {
            if (targetHandler.formation == TargetHandler.FormationMode.LINE)
            {
                rigid.velocity *= 1.01f;
                waterSlowdown = 2;
            }
            else
            {
                waterSlowdown = 0.1f;
                rigid.velocity *= 0.9f;
            }
        }
        /*
        if (isPlayer) //leader
        {
            if (waterState)
            {
                if (targetHandler.formation == TargetHandler.FormationMode.LINE)
                {
                    rigid.velocity *= 1.01f;
                    waterSlowdown = 2;
                }
                else
                {
                    waterSlowdown = 0.1f;
                    rigid.velocity *= 0.9f;
                }
            }
        }
        else
        {
            rigid.velocity *= 1.01f;
             waterSlowdown = 1.5f;
        }
        */

        return waterSlowdown;
    }


    float GetTeleportalSpeed ()
    {
        float teleportalSpeed = 1;
        if (teleportalState)
        {
            teleportalSpeed = 50f;
        }
        else
        {
            teleportalSpeed = 1;
            if (rigid.velocity.magnitude > 50)
            {
                rigid.velocity *= 0.01f;
            }
        }

        return teleportalSpeed;
    }



    void ModSettings_Update(Modifiers.Modifier mod)
    {
        switch (mod)
        {
     //       case Modifiers.Modifier.BOUNCY:
        //        Mod_Bouncy_Update();
        //        break;

            case Modifiers.Modifier.TRIPPY:
                Mod_Trippy_Update();
                break;
        }
    }



    void Mod_Trippy_Update()
    {
        Vector3 scaleChange = initScale;
        scaleChange.x *= (PerlinValue(Time.time + randomSeed, 20) / 2f) + 1.25f;
        scaleChange.y *= (PerlinValue(Time.time + (1.5f * randomSeed), 20) / 2f) + 1.25f;
        transform.localScale = scaleChange;


    }

    void Mod_Bouncy_Update()
    {
        if (Mathf.Abs(rigid.velocity.x) + Mathf.Abs(rigid.velocity.y) > 40)
        {
            rigid.velocity *= 0.8f;
        }
    }







    public void DieAlly ()
    {
        if (!allowDeath) return;
        allowDeath = true;
        tag = "Dead";

        master.scorer.KillAlly();

        StartCoroutine(Explode());
    }

    public void DieNPC ()
    {
        if (!allowDeath) return;
        allowDeath = true;
        tag = "Dead";

        StartCoroutine(Explode());
    }

    public IEnumerator Explode ()
    {
         foreach (Transform child in transform)
         {
             child.GetComponent<Renderer>().enabled = false;
         }

         GetComponent<BoxCollider>().size *= explodeRadius;
         yield return new WaitForSeconds(0.1f);
         Destroy(gameObject);
    }


    void LookAtTarget()
    {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg),
                turnSpeed * Time.deltaTime);

    }
    
    void TargetHandlerMechanic ()
    {
        switch (targetHandler.formation)
        {
            case TargetHandler.FormationMode.LINE:
                turnSpeed = turnSpeedInit * turnSpeedLineFormation;
                forwardSpeed = (forwardInitSpeed + forwardSpeedAdd1) * 1.6f;
                if (drift) allowDrift = false;

                if (isPlayer) rushParticles.enableEmission = true;

                break;
            case TargetHandler.FormationMode.FOLLOW_CURSOR:
                turnSpeed = turnSpeedInit;
                forwardSpeed = (forwardInitSpeed + forwardSpeedAdd1);
                if (drift) allowDrift = true;
                if (isPlayer) rushParticles.enableEmission = false;
                break;

        }

    }


    // HELPER
    float GetTargetDistance()
    {
        return Vector2.Distance(target, transform.position);
    }

    float PerlinValue(float time, float speed) //between -1 and 1
    {
        return (Mathf.PerlinNoise(time * speed / 10f, 0) * 2) - 1;
    }

}
