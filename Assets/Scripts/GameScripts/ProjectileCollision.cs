using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {

    [HideInInspector] public MasterReferences master;
    public float playerPushBackStrength, playerScreenshakeStrength, playerScreenshakeTime;
    public float AllyScreenshakeStrength, AllyScreenshakeTime;
    public GameObject explodeParticles;

    bool playerDied = false;
    public GameObject PlayerDeathParticle;
    public GameObject PointLoseParticle;

    ProjectileMotor projectileMotor;

    private void Start()
    {
        projectileMotor = GetComponent<ProjectileMotor>();
        if (!master)
        {
            master = GameObject.Find("[MASTER]").GetComponent<MasterReferences>();
        }
    }


    private void OnCollisionEnter(Collision coll)
    {
        CheckCollision(coll);
    }

    void CheckCollision(Collision coll)
    {
        GameObject collObj = coll.gameObject;
        if (collObj.tag == "Ally")
        { //|| collObj.tag == "Player")
            master.screenshake.Shake(AllyScreenshakeStrength, AllyScreenshakeTime);
            if (!projectileMotor.isHarmless)
            {
                MoveMotor motor = collObj.GetComponent<MoveMotor>();

                GameObject pointObject = Instantiate(PointLoseParticle, collObj.transform);
                pointObject.transform.parent = pointObject.transform.parent.parent;
                pointObject.transform.localEulerAngles = Vector3.zero;

                motor.DieAlly();

            }

        }

        /* else if (collObj.tag == "NPC")
         {
             MoveMotor motor = collObj.GetComponent<MoveMotor>();
             motor.DieNPC();
         }*/

        else if (collObj.tag == "Player" && !playerDied)
        {

            if (!projectileMotor.isHarmless) { 
                playerDied = true;
                collObj.active = false;
                GameObject particleObject = Instantiate(PlayerDeathParticle, collObj.transform);
                particleObject.transform.parent = transform.root;
                particleObject.transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);
            }
            master.screenshake.Shake(playerScreenshakeStrength, playerScreenshakeTime);






            if (!projectileMotor.isHarmless)
            {
                master.saveHandler.EndGame("DEATH", 1);
            }

        }


        if (collObj.tag != "Enemy")
        {
            GameObject particleObj = Instantiate(explodeParticles, transform);
            particleObj.transform.parent = transform.parent;
            particleObj.GetComponent<ParticleSystem>().Emit(10);

            Destroy(gameObject);
        }
    }
}
