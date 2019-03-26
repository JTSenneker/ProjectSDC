using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramUpgrade : Upgrade
{
    public override void UseUpgrade(bool active)
    {
        base.UseUpgrade(active);
        if (PlayerManager.hasHologram)
        {
            Debug.Log("hologram");
        }
    }
}
