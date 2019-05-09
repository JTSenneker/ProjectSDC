using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playertestpickandpush : MonoBehaviour
{
   
    private Rigidbody Rb;
    private int count;
    public float pickupRange;
    public bool inPickUpRange = false;
    public float fieldOfViewAngle;
    public bool Pushing;
    private GameObject box;
    

    private Collider B_Collider;
    private Collider C_Collider; 
    






    // Use this for initialization
    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        B_Collider = GetComponent<BoxCollider>();
        C_Collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Ybutton") == false)
        {
            Pushing = false;
        }
    }

    void FixedUpdate()
    {
        
    }








    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Abutton"))
        {
            detect(other);
        }
        if (other.gameObject.tag == "Pushable" && Input.GetButton("Ybutton"))
        {
            push(other);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (Input.GetButtonDown("Abutton"))
        {
            inPickUpRange = true;
            detect(other);
        }
        if (other.gameObject.tag == "Pushable" && Input.GetButton("Ybutton"))
        {
           
            push(other);
        }
    }
    void OnTriggerleave(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            inPickUpRange = false;

        }

    }

    void detect(Collider other)
    {
        if (other.gameObject.tag == ("Pickup") || other.gameObject.tag == ("Guard"))
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle <= fieldOfViewAngle * .5f)
            {
                RaycastHit hit;
                C_Collider.enabled = false;
                B_Collider.enabled = false;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, pickupRange))
                {
                    if (hit.collider.gameObject.tag == ("Pickup")&& hit.collider.gameObject.tag != ("Guard"))
                    {
                        Destroy(hit.collider.gameObject);
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                        PointManager.points++;
                        Debug.Log(PointManager.points);
                    }
                }
                else
                {
                    C_Collider.enabled = true;
                    B_Collider.enabled = true;
                }
               C_Collider.enabled = true;
                B_Collider.enabled = true;
            }
        }
    }

    void push(Collider other)
    {
        if (other.gameObject.tag == ("Pushable"))
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle <= fieldOfViewAngle * .5f)
            {
                RaycastHit hit;
                C_Collider.enabled = false;
                B_Collider.enabled = false;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, pickupRange))
                {
                    if (hit.collider.gameObject.tag == ("Pushable"))
                    {
                        Vector3 movement = this.GetComponent<PlayerMovement>().movement;
                        other.attachedRigidbody.AddForce(movement);
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                        Pushing = true;
                    }
                    else
                    {
                       C_Collider.enabled = true;
                        B_Collider.enabled = true;
                        Pushing = false;
                    }
                }
                else
                {
                    C_Collider.enabled = true;
                    B_Collider.enabled = true;
                }
                C_Collider.enabled = true;
                B_Collider.enabled = true;
            }
        }
    }
}
