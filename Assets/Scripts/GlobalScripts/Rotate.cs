using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float xSpeed, ySpeed, zSpeed;

    public float multi = 1;
    public bool randomInversion = false;




    Vector3 randomI = new Vector3(1, 1, 1);

    void Start ()
    {
        if (randomInversion)
        {
            randomI.x = GetRandomInversionFactor();
            randomI.y = GetRandomInversionFactor();
            randomI.z = GetRandomInversionFactor();
        }
    }

	void Update () {
        transform.Rotate(xSpeed * multi * randomI.x, ySpeed * multi * randomI.y, zSpeed * multi * randomI.z);
	}



    int GetRandomInversionFactor ()
    {
        return (Random.Range(0, 2) * 2) - 1;
    }
}
