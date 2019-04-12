using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;
    public bool HologrameMovement;
    public float aimSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled == true)
        {
            HologrameMovement = true;
            if (HologrameMovement)//this is a place holder statement.
            {
                // this will be used to tell if the target is to close to the player.
            }
            if (Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("w"))
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                move(h, v);
            }
        }
        else
        {
            HologrameMovement = false;
        }
    }
    void move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * aimSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
