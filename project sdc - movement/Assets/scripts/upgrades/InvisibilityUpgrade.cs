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
            if (active)
            {
                GameObject.Find("InvisibilityCloak").GetComponent<InvisibilityControllor>().enabled = true;
                Debug.Log("invisibility cloak is on");
            }
            else if (active == false)
            {
                GameObject.Find("InvisibilityCloak").GetComponent<InvisibilityControllor>().enabled = false;
                Debug.Log("invisibility cloak is off");
            }
        }
    }
}
