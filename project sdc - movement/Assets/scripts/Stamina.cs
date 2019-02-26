using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMovement))]
public class Stamina : MonoBehaviour
{
    public static float energy = 100;
    public static float lowestenergy = 30;
    public float regainSpeed;
    PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void FixedUpDate()
    {
        if (energy < 100 && playerMovement.running == false)
        {
            energy += regainSpeed * Time.deltaTime;
        }
    }
}