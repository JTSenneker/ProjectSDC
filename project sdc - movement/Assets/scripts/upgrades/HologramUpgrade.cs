using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramUpgrade : Upgrade
{
    public override void UseUpgrade(bool active,bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasHologram)
        {
            Debug.Log("hologram");
        }
    }
}
