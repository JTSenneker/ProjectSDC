using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramUpgrade : Upgrade
{
    public override void UseUpgrade(bool active,bool activeShield)
    {
        base.UseUpgrade(active, activeShield);
        if (PlayerManager.hasHologram &&/*the following condition makes sure the player has 60% stanima*/ GameObject.Find("player").GetComponent<PlayerStats>().energy >= GameObject.Find("HologramTarget").GetComponent<HologramController>().hologramDepletion)
        {
            if (active)
            {
                GameObject.Find("HologramTarget").GetComponent<Transform>().position = (GameObject.Find("TargetStartPosition").GetComponent<Transform>().position);
                GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled = true;
                //obsolete  //GameObject.Find("HologramTarget").GetComponent<BoxCollider>().enabled = true;
                Debug.Log("Hologram is on");
            }
            else if (active == false)
            {
                GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled = false;
                //obsolete  //GameObject.Find("HologramTarget").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Hologram is off");
            }
        }
    }
}
