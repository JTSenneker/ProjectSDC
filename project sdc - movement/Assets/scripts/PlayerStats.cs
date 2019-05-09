using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public float energy;
    public float lowestenergy;
    public float regainSpeed;
    public float regainDelay;
    float timer;
    PlayerMovement playerMovement;
   
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (timer <= 10)
        {
            timer += Time.deltaTime;
        }
        if (energy <= 100 && timer >= regainDelay && GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled == false && GameObject.Find("InvisibilityCloak").GetComponent<InvisibilityControllor>().enabled == false)
        {
            energy += regainSpeed * Time.deltaTime;
            if(energy > 100)
            {
                energy = 100;
            }
        }
        if(energy <= 0)
        {
            energy = 0;
        }

        if (energy == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
    public void HurtPlayer()
    {
        energy -= 10;
        timer = 0;
    }
    public void TimerReset()
    {
        timer = 0;
    }

    public void DeadPlayer()
    {
      
    }
}
