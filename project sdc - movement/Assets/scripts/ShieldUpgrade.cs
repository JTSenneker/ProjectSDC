using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : Upgrade
{
    public override void useUpgrade()
    {
        base.useUpgrade();
        if (PlayerManager.hasShield)
        {
            Debug.Log("you did it");
        }
    }
}
