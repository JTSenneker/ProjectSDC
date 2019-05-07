using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingTaserUpgrade : Upgrade
{
    public override void UseUpgrade(bool active,bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasExplosiveTaser)
        {
            Debug.Log("exploding taser");
        }
    }
}
