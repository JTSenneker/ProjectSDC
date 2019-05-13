using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerStats playerStats;
    UpgradeList upgradeList;
    HologramController hologramController;
    public Material material;
    public Vector3 movement;
    public float rotate;
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float energyDepletion;
    public float h;
    public float v;
    private float currentSpeed;
    public float fatigueSpeed;
    private int hologram = 1;
    private int shield = 0;
    private int invisibility = 2;
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

        Debug.Log(upgradeList.currentUpgrade);
        if (playerStats.energy <= 30 && active && (upgradeList.upgrades[upgradeList.currentUpgrade] == upgradeList.upgrades[shield] || upgradeList.upgrades[upgradeList.currentUpgrade] == upgradeList.upgrades[invisibility]))
        {
            playerStats.TimerReset();
            active = false;
            upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade(active, activeShield);
            GameObject.Find("player").GetComponent<Renderer>().sharedMaterial = material;
        }
        if (Input.GetButtonDown("Xbutton") && GameObject.Find("HolographicPlayer").GetComponent<MeshRenderer>().enabled == false)
        {
            playerStats.regenStamina = false;
            if (upgradeList.currentUpgrade != 1)
            {
                //playerStats.Invoke("TimerReset", playerStats.regainDelay);
                playerStats.TimerReset();
            }
            print("active = "+active);
            if (active)
            {
                active = false;
                print("Turn off");
                upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade(false, activeShield);
                GameObject.Find("player").GetComponent<Renderer>().sharedMaterial = material;
            }
            else
            {
                print("Turn on");
                upgradeList.upgrades[upgradeList.currentUpgrade].UseUpgrade(true, activeShield);
                active = true;
            }
        }
        if (Input.GetButtonDown("Ybutton"))
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
        if (hologramController.HologrameMovement == true)
        {
            print("player can not move while aiming hologram or during exploding taser");
        }
        else
        {
            //if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
            {
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
                move(h, v);
                currentSpeed = walkSpeed;
            }
            running = false;
        }
    }
    void move(float h, float v)
    {
        if (playerStats.energy >= 30)
        {
            if (crouch)
            {
                currentSpeed = crouchSpeed;
            }
            if (Input.GetButton("rightTrigger") && playerStats.energy > playerStats.lowestenergy && crouch == false)
            {
                playerStats.regenStamina = false;
                playerStats.TimerReset();
                running = true;
                currentSpeed = runSpeed;
                playerStats.energy -= energyDepletion * Time.deltaTime;
                if (playerStats.energy < 30)
                {
                    playerStats.energy = 30;
                }
                //playerStats.Invoke("TimerReset", playerStats.regainDelay);
            }
        }
        else
        {
            currentSpeed = fatigueSpeed;
        }
        movement.Set(h, 0f, v);
        movement = movement.normalized * currentSpeed * Time.deltaTime;
        //rb.MovePosition(transform.position + movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotate);
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
    }
    
}
    