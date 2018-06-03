using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour {

    GameObject player;
    bool parented = false;
    public float speed;
    public bool lookat = true;
    public bool freezePlayer = false;
    Rigidbody rigid;
    MoveMotor motor;

    bool hit;

    void Update()
    {
        if (player)
        {
            if (!hit)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                if (lookat) transform.LookAt(player.transform);
            }

            Debug.Log(gameObject.name + " " + Vector3.Distance(transform.position, player.transform.position));
            if (!hit && Vector3.Distance(transform.position, player.transform.position) < 1 && freezePlayer)
            {
                if (motor)
                {
                    motor.forwardInitSpeed *= 0.6f;
                    motor.forwardSpeed *= 0.6f;
                    transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
                    hit = true;
                }
                else
                {
                    motor = player.GetComponent<MoveMotor>();
                }
            }
        }
        else
        {
            player = GameObject.Find("Player(Clone)");
        }
    }
}
