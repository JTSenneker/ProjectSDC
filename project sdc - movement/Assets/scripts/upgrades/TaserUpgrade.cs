using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserUpgrade : Upgrade
{
    
    public override void UseUpgrade(bool active, bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasTaser)
        {
            GameObject.Find("MeleeAttack").GetComponent<BoxCollider>().enabled=active;
        }
    }
}
