using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody Rb;


    void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        

    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == ("Player"))
        {
            Rb.isKinematic = false;
        }
        else
        {
            Rb.isKinematic = true;
        }
        
    }
    void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == ("Player") && Input.GetButton("Ybutton"))
        {
            Rb.isKinematic = false;
        }
        else
        {
            Rb.isKinematic = true;
        }
        
    }
}
