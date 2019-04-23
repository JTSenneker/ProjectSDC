using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats playerStats;
    UpgradeList upgradeList;
    HologramController hologramController;
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
    public bool active;
    public bool activeShield;//irrelevant code does not do anything. its implamented in about ten different scripts, if removed it will cause a cascade of errors.

    void Start()
    {
        active = false;
        activeShield = false;
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
        playerStats = GetComponent<PlayerStats>();
        upgradeList = GetComponent<UpgradeList>();
        hologramController = GameObject.Find("HologramTarget").GetComponent<HologramController>();
    }
    void Update()
    {
        
        
        if (playerStats.energy <= 30&&active)
        {
            playerStats.TimerReset();
            active = false;
            upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade(active, activeShield);
        }
        if (Input.GetKeyDown("r"))
        {
            playerStats.TimerReset();
            print("active = "+active);
            if (active)
            {
                active = false;
                print("Turn off");
                upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade(false, activeShield);
                
            }
            else
            {
                print("Turn on");
                upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade(true, activeShield);
                active = true;
            }
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
        if (hologramController.HologrameMovement==true)
        {
            print("player can not move while aiming hologram");
        }
        else
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
