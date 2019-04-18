using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public float speed;
    private Rigidbody Rb;
    private int count;
    public float pickupRange;
    public bool inPickUpRange = false;
    public float fieldOfViewAngle;
    public float sightdistance;

    private Collider B_Collider;
    private Collider C_Collider;
    private bool KeyCardCheck;
    private bool GunCheck;
    private bool TaserCheck;






    // Use this for initialization
    void Start() {
        Rb = GetComponent<Rigidbody>();
        B_Collider = GetComponent<BoxCollider>();
        C_Collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() {


    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        Rb.AddForce(movement * speed);
       


    }   








    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
           
           
            detect(other);
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown("e"))
        {
            inPickUpRange = true;
            detect(other);
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
                if (Physics.Raycast(transform.position, direction.normalized, out hit, sightdistance))
                {

                    if (hit.collider.gameObject.tag == ("Pickup") && hit.collider.gameObject.tag != ("Guard"))
                    {
                        Debug.Log("Pickup");
                        Destroy(other.gameObject);
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;

                    }
                    else if (hit.collider.gameObject.tag == ("Guard"))
                    {
                        Debug.Log("Pickpocket");
                        KeyCardCheck = hit.collider.GetComponent<GuardnavcanChase>().hasKeycard;
                        GunCheck = hit.collider.GetComponent<GuardnavcanChase>().hasGun;
                        TaserCheck = hit.collider.GetComponent<GuardnavcanChase>().hasTaser;
                        if (KeyCardCheck == true)
                        {
                            Debug.Log("Get KeyCard");
                            KeyCardCheck = false;
                        }
                        if (GunCheck == true)
                        {
                            Debug.Log("Get Gun");
                            GunCheck = false;
                        }
                        if (TaserCheck == true)
                        {
                            Debug.Log("Get Taser");
                            TaserCheck = false;
                        }
                        hit.collider.GetComponent<GuardnavcanChase>().hasKeycard = KeyCardCheck;
                        hit.collider.GetComponent<GuardnavcanChase>().hasGun = GunCheck;
                        hit.collider.GetComponent<GuardnavcanChase>().hasTaser = TaserCheck;
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
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
