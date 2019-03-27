using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardnavcanChase : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent nav;
    public Transform pathHolder;
    public float speed;
    public float runspeed;
    public float waitTime;
    public float MeleeTime;
    public float GunTime;
    public float TaserTime;
    public float turnSpeed;
    public float fieldOfViewAngle;
    public float shootdistance;
    public float Meleedistance;
    public float Boxdistance;
    public Vector3 movement;
    public Transform[] targetWaypoints;
    float timer;
    float MeleeTimer;
    float GunTimer;
    float TaserTimer;
    public bool hasTaser;
    public bool hasKeycard;
    public bool hasGun;

    public Vector3 LastSighting;
    private Collider B_Collider;
    private Collider C_Collider;
    private static bool ischasing;

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

            gameObject.tag = ("Guard");

        }


        if (Vector3.Distance(LastSighting, start) < .05f)
        {
            NextPoint();

        }
        else
        {
            if (Vector3.Distance(transform.position, LastSighting) < .5f)
            {
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
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard"))
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
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard"))
        {

            detect(other);

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard"))
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
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard"))
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
                                if (hasGun == true)
                                {
                                    GunTimer += Time.deltaTime;
                                    if (timer >= GunTime)
                                    {
                                        GunTimer = 0;
                                        Debug.Log("Shoot");
                                    }

                                }
                                else if (hasTaser == true)
                                {
                                    TaserTimer += Time.deltaTime;
                                    if (timer >= TaserTime)
                                    {
                                        TaserTimer = 0;
                                        Debug.Log("Taser");
                                    }
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
                            LastSighting = other.GetComponent<GuardnavcanChase>().LastSighting;
                        }
                        C_Collider.enabled = true;
                        B_Collider.enabled = true;
                    }
                    else if (hit.collider.gameObject.tag == ("ChasingcamGuard"))
                    {
                        if (ischasing == false)
                        {
                            LastSighting = other.GetComponent<CameraSpiderAI>().LastSighting;
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
