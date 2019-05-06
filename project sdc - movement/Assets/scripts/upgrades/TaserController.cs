using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserController : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerStats playerStats;
    public float wait;
    public float attackTime;
    public float taserDepletion;
    void Start()
    {
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Guard")
        {
            print("player grabs enemy and incapacitates him");
            playerStats.energy -= taserDepletion;
            if (playerStats.energy < 0)
            {
                playerStats.energy = 0;
            }
            playerMovement.active = false;
            wait = 0;
            GameObject.Find("MeleeAttack").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("MeleeAttack").GetComponent<TaserController>().enabled = false;
            other.gameObject.GetComponent<GuardAIv2>().stunned = true;
        }
    }
    void FixedUpdate()
    {
        wait += Time.deltaTime;
        if (wait>attackTime)
        {
            playerMovement.active = false;
            wait = 0;
            GameObject.Find("MeleeAttack").GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("MeleeAttack").GetComponent<TaserController>().enabled = false;
        }
    }
}
