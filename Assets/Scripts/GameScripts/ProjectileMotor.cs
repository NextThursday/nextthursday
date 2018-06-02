using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotor : MonoBehaviour {

    [Header("REFERENCES")]
    public Rigidbody rigid;

    [Header("CONTROLS")]
    public float speed;
    
    public float speedMulti = 1;

    public bool isHarmless;

    void Update () {
        rigid.AddForce(transform.right * speed * speedMulti);
	}
}
