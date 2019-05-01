using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingTaserController : MonoBehaviour
{
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    HologramController hologramController;// this is reffered to as the 'hologramcontroller' but has nothing specific to do with the hologram and its only function is to stop the play from moving.
    public int explodingTaserDepletion;
    void Start()
    {
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        hologramController = GameObject.Find("HologramTarget").GetComponent<HologramController>();
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
    }
    void Update()
    {
        {
            hologramController.HologrameMovement = true;
            playerStats.energy -= explodingTaserDepletion;
            /*need to add animation with hit box activation*/
            hologramController.HologrameMovement = false;
            playerMovement.active = false;
            playerStats.TimerReset();
            GameObject.Find("ExplodingTaser").GetComponent<ExplodingTaserController>().enabled = false;
        }
    }
}
