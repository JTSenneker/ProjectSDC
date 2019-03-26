﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityUpgrade : Upgrade
{
    public override void UseUpgrade(bool active)
    {
        base.UseUpgrade(active);
        if (PlayerManager.hasInvisibilityCloak)
        {
            Debug.Log("invisibility cloak");
        }
    }
}
