using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour {

    GameObject player;
    bool parented = false;

	void Update () {
        if (player)
        {
            if (!parented)
            {
                parented = true;
                transform.parent = player.transform;
            }

        } else
        {
            player = GameObject.Find("Player(Clone)");
        }
	}
}
