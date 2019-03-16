using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;
    public float rotate;
    public float walkSpeed;
    public float runSpeed;
    public float crouchspeed;
    public float energydepletion;
    private float currentSpeed;
    private Rigidbody rb;
    private Quaternion Quat;
    private bool crouch = false;
    public bool running;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        GetComponent<Stamina>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (PlayerManager.hasHologramUpgrade)
            {
                print("I have the hologram");
            }
            if (crouch == false)
            {
                crouch = true;
            }
            else
            {
                crouch = false;
            }
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey("a")||Input.GetKey("s")||Input.GetKey("d")||Input.GetKey("w"))
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            move(h, v);
            currentSpeed = walkSpeed;
        }
        running = false;
    }
    void move(float h,float v)
    {
        if(crouch)
        {
            currentSpeed = crouchspeed;
        }
        if (Input.GetKey(KeyCode.LeftShift)&&Stamina.energy > Stamina.lowestenergy && crouch == false)
        {
            running = true;
            currentSpeed = runSpeed;
            Stamina.energy -= energydepletion*Time.deltaTime;
            if(Stamina.energy<30)
            {
                Stamina.energy = 30;
            }
        }
        movement.Set(h, 0f, v);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movement),rotate);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
    }
}
