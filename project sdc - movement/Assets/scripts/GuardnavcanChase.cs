using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardnavcanChase : MonoBehaviour {
    UnityEngine.AI.NavMeshAgent nav;
    public Transform pathHolder;
    public float speed;
    public float runspeed;
    public float waitTime;
    public float turnSpeed;
    public float fieldOfViewAngle;
    public float sightdistance;
    public Vector3 movement;
    public Transform[] targetWaypoints;
    float timer;

    private Collider B_Collider;
    private Collider C_Collider;
    private static bool ischasing;
    
    private Transform player;
    private Transform up;
    private int nextpoint=1;
    private int totalPoints=0;
    float viewAngle;
    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
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
            NextPoint();
            gameObject.tag = ("Guard");
        }
    }

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];

        ischasing = false;
        
        B_Collider = GetComponent<BoxCollider>();
        C_Collider = GetComponent<CapsuleCollider>();
    }


    void NextPoint()
    {
        nav.SetDestination(targetWaypoints[nextpoint].position);
        if (Vector3.Distance(transform.position, targetWaypoints[nextpoint].position)<=.05f)
        {
            timer += Time.deltaTime;
            Vector3 dirTurnToLookTarget = (targetWaypoints[(nextpoint + 1) % targetWaypoints.Length].position - transform.position).normalized;
            float targetAngle = 90 - Mathf.Atan2(dirTurnToLookTarget.z, dirTurnToLookTarget.x) * Mathf.Rad2Deg;
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            if (timer>= waitTime)
            {
                timer = 0;
                nextpoint = (nextpoint + 1) % targetWaypoints.Length;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        detect(other);
       
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            ischasing = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        detect(other);
    }

    void detect(Collider other)
    {
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard"))
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

                    if (hit.collider.gameObject.tag == ("Player") || hit.collider.gameObject.tag == ("ChasingGuard"))
                    {
                        Debug.Log("sight");
                        ischasing = true;
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
            }
           
        }
        else
        {
            ischasing = false;
        }
    }
    




}


