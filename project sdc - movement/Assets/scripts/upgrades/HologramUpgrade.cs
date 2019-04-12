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
            if (active)
            {
                GameObject.Find("HologramTarget").GetComponent<Transform>().position = (GameObject.Find("TargetStartPosition").GetComponent<Transform>().position);
                GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("HologramTarget").GetComponent<BoxCollider>().enabled = true;
                Debug.Log("Hologram is on");
            }
            else if (active == false)
            {
                GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("HologramTarget").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("Hologram is off");
            }
        }
    }
}
