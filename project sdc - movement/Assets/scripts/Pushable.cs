using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

   
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
        Debug.Log("test");
        if (other.gameObject.tag == ("Player") && Input.GetKey("p"))
        {
            Rb.isKinematic = false;
        }
        else
        {
            Rb.isKinematic = true;
        }
        
    }
    void OnTriggersStay(Collider other)
    {
        Debug.Log("test");
        if (other.gameObject.tag == ("Player") && Input.GetKey("p"))
        {
            Rb.isKinematic = false;
        }
        else
        {
            Rb.isKinematic = true;
        }
        
    }
}
