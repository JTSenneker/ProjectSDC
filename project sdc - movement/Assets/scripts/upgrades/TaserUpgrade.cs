using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserUpgrade : Upgrade
{
    
    public override void UseUpgrade(bool active, bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasTaser &&/*the following condition makes sure the player has 50% stanima*/ GameObject.Find("player").GetComponent<PlayerStats>().energy >= GameObject.Find("MeleeAttack").GetComponent<TaserController>().taserDepletion)
        {
            GameObject.Find("MeleeAttack").GetComponent<BoxCollider>().enabled = true;
            GameObject.Find("MeleeAttack").GetComponent<TaserController>().enabled = true;
        }
    }
}
