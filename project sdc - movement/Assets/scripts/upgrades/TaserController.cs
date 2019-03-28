using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserController : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Untagged")
        {
            Debug.Log("triggered");
        }
    }
}
