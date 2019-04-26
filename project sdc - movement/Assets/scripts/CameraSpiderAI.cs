using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpiderAI : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    public float turnSpeed;
    public float fieldOfViewAngle;
    public float sightdistance;
    public Transform[] targetWaypoints;
    float timer;
    public float waitTime;
    public float lungeSpeed;
    public float waitAfterLunge;
    public float lookSway;
    public float LookAverageDir;
    public Vector3 LastSighting;

    private Vector3 startspot;
    private Vector3 Lungespot;
    private Vector3 wallSpot;
    public bool ischasing;
    private Collider Orb_Collider;
    private Collider B_Collider;
    private Collider C_Collider;
    private Transform player;
    private int nextpoint = 1;
    private int totalPoints = 0;
    private bool islunging;
    float viewAngle;
    private float basespeed;
    private float lungetimer;

    void Awake()
    {
        startspot = this.transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3[] waypoints = new Vector3[targetWaypoints.Length];
        ischasing = false;
        wallSpot = startspot;
        basespeed = GetComponent<UnityEngine.AI.NavMeshAgent>().speed;

        Orb_Collider = GetComponent<SphereCollider>();
        B_Collider = GetComponent<BoxCollider>();
        C_Collider = GetComponent<CapsuleCollider>();
        Orb_Collider.enabled = false;
        LastSighting = transform.position;
        LastSighting.y += .1f;
        islunging = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (ischasing == true)
        {
            gameObject.tag = ("ChasingcamGuard");
            if (Vector3.Distance(transform.position, wallSpot) < .15f)
            {
                if (islunging == false)
                {

                    nav.SetDestination(Lungespot);
                    islunging = true;
                    nav.speed = lungeSpeed;
                }
            }
            else if (islunging == true && Vector3.Distance(transform.position, Lungespot) < .5f)
            {
                Debug.Log(Lungespot);
                lungetimer += Time.deltaTime;
                nav.speed = 0;

                if (lungetimer >= waitAfterLunge)
                {
                    islunging = false;
                    Debug.Log("resume chase");
                    Lungespot = wallSpot;
                    nav.speed = basespeed;
                    lungetimer = 0;
                }

            }
            else if (islunging == false)
            {
                nav.SetDestination(LastSighting);
            }
        }

        if (ischasing == false)
        {
            gameObject.tag = ("Guard");
        }

        if (Vector3.Distance(LastSighting, wallSpot) < .05f)
        {
            if (Vector3.Distance(transform.position, wallSpot) < .15f)
            {
                float angle = Mathf.Sin(Time.time) * lookSway + LookAverageDir;
                Vector3 eulers = new Vector3(0, angle, 0);
                transform.rotation = Quaternion.Euler(eulers);
            }
            else
            {

                nav.SetDestination(wallSpot);
            }

        }
        else
        {
            if (Vector3.Distance(transform.position, LastSighting) < .5f)
            {
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    timer = 0;
                    nav.SetDestination(wallSpot);
                    LastSighting = wallSpot;
                    ischasing = false;
                }
            }
            else if (islunging == false)
            {
                nav.SetDestination(LastSighting);
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        detect(other);

    }
    void OnTriggerStay(Collider other)
    {
        detect(other);

    }

    void detect(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("holoPlayer"))
        {

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle <= fieldOfViewAngle * .5f)
            {

                RaycastHit hit;
                C_Collider.enabled = false;
                B_Collider.enabled = false;
                Orb_Collider.enabled = false;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, sightdistance))
                {

                    if (hit.collider.gameObject.tag == ("Player"))
                    {

                        Debug.Log("sight");
                        LastSighting = player.position;
                        if (Vector3.Distance(transform.position, wallSpot) < .15f)
                        {
                            Lungespot = LastSighting;
                        }

                        Orb_Collider.enabled = true;
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                        ischasing = true;

                    }
                    else if (hit.collider.gameObject.tag == ("holoPlayer"))
                    {

                        Debug.Log("sight");
                        LastSighting = hit.collider.gameObject.transform.position;
                        if (Vector3.Distance(transform.position, wallSpot) < .15f)
                        {
                            Lungespot = LastSighting;
                        }

                        Orb_Collider.enabled = true;
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                        ischasing = true;

                    }
                    else
                    {
                        Orb_Collider.enabled = false;
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;

                    }
                }
                else
                {
                    Orb_Collider.enabled = false;
                    C_Collider.enabled = true;
                    B_Collider.enabled = true;
                }
            }


        }

    }

}