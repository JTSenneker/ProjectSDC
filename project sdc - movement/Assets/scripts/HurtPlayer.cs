using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    PlayerStats playerStats;
    void Start()
    {
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerStats.HurtPlayer();
        }
    }
}
