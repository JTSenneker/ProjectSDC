using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingTaserUpgrade : Upgrade
{
    public override void UseUpgrade(bool active,bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasExplosiveTaser &&/*the following condition makes sure the player has 100% stanima*/ GameObject.Find("player").GetComponent<PlayerStats>().energy == GameObject.Find("ExplodingTaser").GetComponent<ExplodingTaserController>().explodingTaserDepletion)
        {
            Debug.Log("exploding taser");
            GameObject.Find("ExplodingTaser").GetComponent<ExplodingTaserController>().enabled = true;
        }
    }
}
