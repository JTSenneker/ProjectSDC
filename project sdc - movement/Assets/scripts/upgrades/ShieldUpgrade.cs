﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : Upgrade
{
    public override void UseUpgrade(bool active, bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasShield)
        {
            Debug.Log("shield");
            if (active)
            {
                GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Shield").GetComponent<BoxCollider>().enabled = true;
                Debug.Log("shield is on");
            }
            else if(active == false)
            {
                GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Shield").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("shield is off");
            }
        }
    }
}
