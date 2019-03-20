using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingTaserUpgrade : Upgrade
{
    public override void UseUpgrade()
    {
        base.UseUpgrade();
        if (PlayerManager.hasExplosiveTaser)
        {
            Debug.Log("exploding taser");
        }
    }
}
