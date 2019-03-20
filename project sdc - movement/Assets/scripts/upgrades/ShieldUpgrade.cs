using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : Upgrade
{
    public override void UseUpgrade()
    {
        base.UseUpgrade();
        if (PlayerManager.hasShield)
        {
            Debug.Log("shield");
        }
    }
}
