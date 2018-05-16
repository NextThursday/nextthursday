using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour {

    [HideInInspector] public MasterReferences master;
    public float playerPushBackStrength, playerScreenshakeStrength, playerScreenshakeTime;
    public float AllyScreenshakeStrength, AllyScreenshakeTime;

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
            MoveMotor motor = collObj.GetComponent<MoveMotor>();
            motor.DieAlly();
        }

       /* else if (collObj.tag == "NPC")
        {
            MoveMotor motor = collObj.GetComponent<MoveMotor>();
            motor.DieNPC();
        }*/

        else if (collObj.tag == "Player")
        {
            master.saveHandler.EndGame("DEATH");
            /*
            master.screenshake.Shake(playerScreenshakeStrength, playerScreenshakeTime);
            Vector3 dirToPlayer = -transform.right;
            collObj.GetComponent<Rigidbody2D>().AddForce(dirToPlayer * -playerPushBackStrength);
            Debug.Log("OUCH!");
            Debug.Break();*/
        }


        if (collObj.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
