using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportal : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Ally")
        {
            coll.gameObject.GetComponent<MoveMotor>().isInTeleportal(true);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Ally")
        {
            coll.gameObject.GetComponent<MoveMotor>().isInTeleportal(false);
        }
    }
}
