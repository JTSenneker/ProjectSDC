using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserUpgrade : Upgrade
{
    public override void UseUpgrade()
    {
        base.UseUpgrade();
        if (PlayerManager.hasTaser)
        {
            Debug.Log("taser");
        }
    }
}
