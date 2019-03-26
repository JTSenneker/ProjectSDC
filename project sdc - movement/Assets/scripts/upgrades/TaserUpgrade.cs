﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserUpgrade : Upgrade
{
    public override void UseUpgrade(bool active)
    {
        base.UseUpgrade(active);
        if (PlayerManager.hasTaser)
        {
            Debug.Log("taser");
        }
    }
}
