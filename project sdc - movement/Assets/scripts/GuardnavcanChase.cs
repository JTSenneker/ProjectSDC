using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardnavcanChase : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent nav;
    public Transform pathHolder;
    public float speed;
    public float runspeed;
    public float waitTime;
    public float MeleeTime;
    public float GunTime;
    public float turnSpeed;
    public float fieldOfViewAngle;
    public float shootangle;
    public float shootdistance;
    public float Meleedistance;
    public Vector3 movement;
    public Transform[] targetWaypoints;
    float timer;
    float MeleeTimer;
    float GunTimer;
    float TaserTimer;
    public float Boxdistance;




    public Vector3 LastSighting;
    private Collider B_Collider;
    private Collider C_Collider;
    public bool ischasing;
    private bool ismelee;
    private bool isshoot;
    private Transform player;
    private Vector3 start;
    private Transform up;
    private int nextpoint = 1;
    private int totalPoints = 0;
    float viewAngle;
    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(1).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {

            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;

        }
        Gizmos.DrawLine(previousPosition, startPosition);

    }



    void Update()
    {

        if (ischasing == true)
        {
            
            nav.SetDestination(player.position);
            gameObject.tag = ("ChasingGuard");
        }
        if (ischasing == false)
        {
            isshoot = false;
            ismelee = false;
            gameObject.tag = ("Guard");

        }
        if (ismelee == true)
        {

            MeleeTimer += Time.deltaTime;

        }
        else
        {
            MeleeTimer = 0;
        }

        if (MeleeTimer >= MeleeTime)
        {
            if (ismelee == true)
            {
                Stamina.energy = Stamina.energy - 50;
                Debug.Log(Stamina.energy);
                MeleeTimer = 0;
                ismelee = false;
            }
            MeleeTimer = 0;

        }


        if (isshoot == true)
        {

            GunTimer += Time.deltaTime;

        }
        else
        {
            GunTimer = 0;
        }

        if (GunTimer >= GunTime)
        {
            if (isshoot == true)
            {
                Debug.Log("Shoot");
                Stamina.energy = Stamina.energy - 30;
                Debug.Log(Stamina.energy);
                GunTimer = 0;
                isshoot = false;
            }
            GunTimer = 0;

        }


        if (Vector3.Distance(LastSighting, start) < .05f)
        {
            NextPoint();
            ischasing = false;
        }
        else
        {
            if (Vector3.Distance(transform.position, LastSighting) < .2f)
            {
                ischasing = false;
                timer += Time.deltaTime;
                if (timer >= waitTime)
                {
                    timer = 0;
                    LastSighting = start;
                    NextPoint();
                }
            }
            else
            {
                nav.SetDestination(LastSighting);
            }
        }
    }

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];

        ischasing = false;
        start = player.position;
        B_Collider = GetComponent<BoxCollider>();
        C_Collider = GetComponent<CapsuleCollider>();
        player.GetComponent<Stamina>();
        LastSighting = start;
    }


    void NextPoint()
    {
        nav.SetDestination(targetWaypoints[nextpoint].position);
        if (Vector3.Distance(transform.position, targetWaypoints[nextpoint].position) <= .05f)
        {
            timer += Time.deltaTime;
            Vector3 dirTurnToLookTarget = (targetWaypoints[(nextpoint + 1) % targetWaypoints.Length].position - transform.position).normalized;
            float targetAngle = 90 - Mathf.Atan2(dirTurnToLookTarget.z, dirTurnToLookTarget.x) * Mathf.Rad2Deg;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            if (timer >= waitTime)
            {
                timer = 0;
                nextpoint = (nextpoint + 1) % targetWaypoints.Length;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer"))
        {
            detect(other);

        }

        if (other.gameObject.tag == ("Pushable"))
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            C_Collider.enabled = false;
            B_Collider.enabled = false;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, Boxdistance))
            {
                Destroy(other.gameObject);
                C_Collider.enabled = true;
                B_Collider.enabled = true;
            }
            C_Collider.enabled = true;
            B_Collider.enabled = true;
        }

    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer"))
        {

            detect(other);

        }
        if (other.gameObject.tag == ("Player"))
        {
            ischasing = false;
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer"))
        {

            detect(other);

        }
        if (other.gameObject.tag == ("Pushable"))
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            C_Collider.enabled = false;
            B_Collider.enabled = false;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, Boxdistance))
            {
                Destroy(other.gameObject);
                C_Collider.enabled = true;
                B_Collider.enabled = true;
            }
            C_Collider.enabled = true;
            B_Collider.enabled = true;
        }
    }

    void detect(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer"))
        {

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle <= fieldOfViewAngle * .5f)
            {

                RaycastHit hit;
                C_Collider.enabled = false;
                B_Collider.enabled = false;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, 10000f))
                {

                    if (hit.collider.gameObject.tag == ("Player"))
                    {
                        Debug.Log("sight");
                        ischasing = true;
                        LastSighting = player.position;
                        if (Physics.Raycast(transform.position, direction.normalized, out hit, Meleedistance))
                        {
                            ismelee = true;
                            isshoot = false;

                        }
                        else if (Physics.Raycast(transform.position, direction.normalized, out hit, shootdistance) && angle <= shootangle * .5f)
                        {
                            if (Physics.Raycast(transform.position, direction.normalized, out hit, Meleedistance))
                            {
                                ismelee = true;
                                isshoot = false;

                            }
                            else
                            {
                                isshoot = true;
                                ismelee = false;

                            }
                        }
                        else
                        {
                            isshoot = false;
                            ismelee = false;
                        }
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                    }
                    else if (hit.collider.gameObject.tag == ("holoPlayer"))
                    {
                        Debug.Log("holosight");
                        ischasing = true;
                        LastSighting = other.gameObject.transform.position;
                        if (Physics.Raycast(transform.position, direction.normalized, out hit, shootdistance))
                        {
                            if (Physics.Raycast(transform.position, direction.normalized, out hit, Meleedistance))
                            {
                                MeleeTimer += Time.deltaTime;
                                if (timer >= MeleeTime)
                                {
                                    MeleeTimer = 0;
                                    Debug.Log("Melee");
                                }

                            }
                            else
                            {

                                GunTimer += Time.deltaTime;
                                if (timer >= GunTime)
                                {
                                    GunTimer = 0;
                                    Debug.Log("Shoot");
                                }
                            }
                        }
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                    }
                    else if (hit.collider.gameObject.tag == ("ChasingGuard"))
                    {
                        if (ischasing == false)
                        {
                            LastSighting = hit.collider.gameObject.GetComponent<GuardnavcanChase>().LastSighting;
                            isshoot = false;
                            ismelee = false;
                        }
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                    }
                    else if (hit.collider.gameObject.tag == ("ChasingcamGuard"))
                    {
                        if (ischasing == false)
                        {
                            if (hit.collider.gameObject.GetComponent<CameraSpiderAI>().LastSighting != null)
                            {
                                LastSighting = hit.collider.gameObject.GetComponent<CameraSpiderAI>().LastSighting;
                            }
                        }
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                    }
                    else
                    {
                        ischasing = false;
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;

                    }
                }
                else
                {
                    ischasing = false;
                    C_Collider.enabled = true;
                    B_Collider.enabled = true;

                }

                C_Collider.enabled = true;
                B_Collider.enabled = true;

            }
            else
            {
                ischasing = false;
                C_Collider.enabled = true;
                B_Collider.enabled = true;

            }

        }
        else
        {
            ischasing = false;
            C_Collider.enabled = true;
            B_Collider.enabled = true;

        }
    }

}


