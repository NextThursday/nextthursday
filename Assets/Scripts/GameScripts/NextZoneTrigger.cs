using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextZoneTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider coll)
    {
        CheckCollision(coll);
    }


    void CheckCollision(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Application.LoadLevel(Application.loadedLevel);
            //GameObject.Find("[MASTER]").GetComponent<MasterReferences>().saveHandler.NextScene();
        }
    }

}
