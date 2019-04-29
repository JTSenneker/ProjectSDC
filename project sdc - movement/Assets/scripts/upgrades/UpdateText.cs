using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    Text text;
    PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = playerStats.energy.ToString();
    }
}
