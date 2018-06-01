using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentCustom : MonoBehaviour {

    public GameObject OpenParticle;
    public GameObject PlayerParticle;
    bool destroyed = false;
    bool scaleDown = false;

    private void OnCollisionEnter(Collision coll)
    {
        CheckCollision(coll);
    }

    void CheckCollision(Collision coll)
    {
        if (destroyed) return;
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerScript>().GetGift();
            DestroyGift(coll);
        }
    }

    void DestroyGift (Collision coll)
    {
        destroyed = true;
        GameObject particleObject = Instantiate(OpenParticle, transform);
        particleObject.transform.parent = transform.root;
        particleObject.transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);

        GameObject particleObject2 = Instantiate(PlayerParticle, coll.transform);
        particleObject2.transform.parent = transform.root;
        particleObject2.transform.Translate(new Vector3(0, 0, -1));
        particleObject2.transform.GetChild(0).GetComponent<ParticleSystem>().Emit(1);



        StartCoroutine(DestroyGiftTiming());
    }

    IEnumerator DestroyGiftTiming ()
    {
        yield return new WaitForSeconds(0.1f);
        scaleDown = true;
    }


    float scaleDownCounter = 0;

    private void Update()
    {
        if (scaleDown)
        {
            scaleDownCounter += 6 * Time.deltaTime;

            float scale = -scaleDownCounter + 2;

            transform.localScale = new Vector3 (scale, scale, scale);
            Debug.Log(scale + " scale down");
            if (scale < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }

}
