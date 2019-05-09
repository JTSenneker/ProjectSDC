using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityControllor : MonoBehaviour
{
    PlayerStats playerStats;
    public Material material;

    public float invisibilityDepletion;
    void Awake()
    {
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
    }
    void Update()
    {
        if (GameObject.Find("InvisibilityCloak").GetComponent<InvisibilityControllor>().enabled == true)
        {
            GameObject.Find("player").GetComponent<Renderer>().sharedMaterial = material;
            playerStats.energy -= invisibilityDepletion * Time.deltaTime;
            if (playerStats.energy < 30)
            {
                playerStats.energy = 30;
            }
        }
    }
}
