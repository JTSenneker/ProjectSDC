using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramUpgrade : Upgrade
{
    public override void UseUpgrade()
    {
        base.UseUpgrade();
        if (PlayerManager.hasHologram)
        {
            Debug.Log("hologram");
        }
    }
}
