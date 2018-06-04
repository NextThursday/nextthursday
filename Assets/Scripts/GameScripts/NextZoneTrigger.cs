using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextZoneTrigger : MonoBehaviour
{
    public float delay;
    public bool startCalender;
    bool loaded;


    private void OnTriggerEnter(Collider coll)
    {
        CheckCollision(coll);
    }


    void CheckCollision(Collider coll)
    {
        if (coll.gameObject.tag == "Player" && !loaded)
        {
            loaded = true;
            StartCoroutine(NextZone());
            
        }
    }


    IEnumerator NextZone ()
    {
        if (startCalender)
        {
            MasterReferences master = GameObject.Find("[MASTER]").GetComponent<MasterReferences>();
            master.controls.weekLength = delay;
            master.controls.isTutorial = false;
            master.controls.loadNextScene = false;
            master.countdown.StartCount();
        }

        yield return new WaitForSeconds(delay);
        Application.LoadLevel(Application.loadedLevel);
    }
}
