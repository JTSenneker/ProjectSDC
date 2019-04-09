using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityControllor : MonoBehaviour
{
    PlayerStats playerStats;
    public float invisibilityDepletion;
    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        Debug.Log("invisibility has playerstats");
    }
    void Update()
    {
        playerStats.energy -= invisibilityDepletion * Time.deltaTime;
        if (playerStats.energy < 30)
        {
            playerStats.energy = 30;
        }
    }
}
