using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearChildren : MonoBehaviour {

    public float delay;

	void Start () {
        SetChildren(false);
        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay (float delay)
    {
        yield return new WaitForSeconds(delay);
        SetChildren(true);
    }


    void SetChildren (bool state)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}
