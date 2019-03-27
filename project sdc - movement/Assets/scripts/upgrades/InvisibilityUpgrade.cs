using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityUpgrade : Upgrade
{
    public override void UseUpgrade(bool active, bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasInvisibilityCloak)
        {
            Debug.Log("invisibility cloak");
        }
    }
}
