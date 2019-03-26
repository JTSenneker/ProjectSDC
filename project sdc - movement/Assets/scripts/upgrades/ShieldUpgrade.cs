using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : Upgrade
{
    public override void UseUpgrade(bool active)
    {
        base.UseUpgrade(active);
        if (PlayerManager.hasShield)
        {
            Debug.Log("shield");
            if (active)
            {
                GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("Shield").GetComponent<BoxCollider>().enabled = true;
                Debug.Log("on");
            }
            else if(active == false)
            {
                GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find("Shield").GetComponent<BoxCollider>().enabled = false;
                Debug.Log("off");
            }
        }
    }
}
