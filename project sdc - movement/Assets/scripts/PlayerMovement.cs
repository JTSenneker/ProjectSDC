using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats playerStats;
    UpgradeList upgradeList;
    Vector3 movement;
    public float rotate;
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float energyDepletion;
    private float currentSpeed;
    public float fatigueSpeed;
    private Rigidbody rb;
    private Quaternion Quat;
    private bool crouch = false;
    private bool running = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        playerStats = GetComponent<PlayerStats>();
        upgradeList = GetComponent<UpgradeList>();
    }
    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
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
            if (Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("w"))
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
        if (playerStats.energy >= 30)
        {
            if (crouch)
            {
                currentSpeed = crouchSpeed;
            }
            if (Input.GetKey(KeyCode.LeftShift) && playerStats.energy > playerStats.lowestenergy && crouch == false)
            {
                playerStats.TimerReset();
                running = true;
                currentSpeed = runSpeed;
                playerStats.energy -= energyDepletion * Time.deltaTime;
                if (playerStats.energy < 30)
                {
                    playerStats.energy = 30;
                }
            }
        }
        else
        {
            currentSpeed = fatigueSpeed;
        }
        movement.Set(h, 0f, v);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movement),rotate);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
    }
}
