using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingTaserController : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    HologramController hologramController;// this is reffered to as the 'hologramcontroller' but has nothing specific to do with the hologram and its only function is to stop the play from moving.
    public Animator anim;
    public int explodingTaserDepletion;
    void Start()
    {
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        hologramController = GameObject.Find("HologramTarget").GetComponent<HologramController>();
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
        anim = GameObject.Find("player").GetComponent<Animator>();
    }
    void Update()
    {
        {
            hologramController.HologrameMovement = true;
            playerStats.energy -= explodingTaserDepletion;
            anim.SetTrigger("punchDown");
            //hologramController.HologrameMovement = false;
            //playerStats.regenStamina = false;
            //playerMovement.active = false;
            playerStats.TimerReset();
            Invoke("disable", 1);
        
            //GameObject.Find("ExplodingTaser").GetComponent<ExplodingTaserController>().enabled = false;
        }
    }
    void LateUpdate()
    {
        //GameObject.Find("ExplodingTaser").GetComponent<ExplodingTaserController>().enabled = false;
    }
    void disable()
    {
        enabled = false;
    }
}
