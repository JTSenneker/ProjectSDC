using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAIv2 : MonoBehaviour
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
    public float Meleeangle;
    public Vector3 movement;
    public Transform[] targetWaypoints;
    public Vector3 targetpoint;
    float timer;
    float MeleeTimer;
    float GunTimer;
    float TaserTimer;
    public float stuntime;
    public float stuntimer;
    public bool stunned;
    public float Boxdistance;
    public bool atplayer;
    public PlayerStats stats;
    ParticleSystem shotParticles;
    LineRenderer shotLine;
    Light shotlight;




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
    private float shotlinetime = .1f;
    private float shotlinetimer;
    private bool shotlineOn;


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

    void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];

        ischasing = false;
        start = player.position;
        B_Collider = GetComponent<BoxCollider>();
        C_Collider = GetComponent<CapsuleCollider>();
        stats = player.GetComponent<PlayerStats>();
        shotParticles = GetComponent<ParticleSystem>();
        shotLine = GetComponent<LineRenderer>();
        shotlight = GetComponent<Light>();
        LastSighting = start;
        nav.speed = speed;
        stunned = false;
    }

    void Update()
    {
        if (stunned == true)
        {

            ischasing = false;
            isshoot = false;
            ismelee = false;
            nav.speed = 0;
            stuntimer += Time.deltaTime;
            if (stuntimer >= stuntime)
            {
                stunned = false;
                stuntimer = 0;
                nav.speed = speed;
            }
        }



        if (ischasing == true && stunned == false)
        {
            nav.speed = runspeed;
            nav.SetDestination(LastSighting);
            gameObject.tag = ("ChasingGuard");
        }
        if (ischasing == false && stunned == false)
        {
            isshoot = false;
            ismelee = false;
            gameObject.tag = ("Guard");
            nav.speed = speed;
        }
        if (ismelee == true && stunned == false)
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
                if (atplayer == true)
                {

                    stats.energy = stats.energy - 50;
                    stats.timer = 0;

                }
                MeleeTimer = 0;
                ismelee = false;
            }
            MeleeTimer = 0;

        }


        if (isshoot == true && stunned == false)
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
                Shoot();
                stats.timer = 0;
                isshoot = false;
                shotlineOn=true;
                GunTimer = 0;
            }
            
        }

        if (shotlineOn == true)
        {
            shotlinetimer += Time.deltaTime;
            if (shotlinetimer >= shotlinetime)
            {
                shotlight.enabled = false;
                shotLine.enabled = false;
                shotlineOn = false;
                shotlinetimer = 0;
            }
        }

        if (Vector3.Distance(LastSighting, start) < .05f && stunned == false)
        {
            NextPoint();
            ischasing = false;
        }
        else if (stunned == false)
        {
            if (Vector3.Distance(transform.position, LastSighting) < .5f)
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




    void NextPoint()
    {
        nav.SetDestination(targetWaypoints[nextpoint].position);
        if (Vector3.Distance(transform.position, targetWaypoints[nextpoint].position) <= .3f)
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
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer") || other.gameObject.tag == ("holoShield"))
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
        if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer") || other.gameObject.tag == ("holoShield"))
        {

            detect(other);

        }
        if (other.gameObject.tag == ("Pushable"))
        {
            RaycastHit hit;
            Vector3 direction = transform.forward;
            C_Collider.enabled = false;
            B_Collider.enabled = false;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, Boxdistance))  //so the player can't use pushables as shields
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
        if (stunned == false)
        {
            if (other.gameObject.tag == ("Player") || other.gameObject.tag == ("ChasingGuard") || other.gameObject.tag == ("ChasingcamGuard") || other.gameObject.tag == ("holoPlayer") || other.gameObject.tag == ("holoShield"))
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
                        if (hit.collider.gameObject.tag == ("Pushable"))
                        {
                            print("hello");
                        }
                        if (hit.collider.gameObject.tag == ("Player") && hit.collider.gameObject.tag != ("Pushable"))
                        {
                            if (Vector3.Distance(transform.position, hit.transform.position) < 1.55f && angle <= shootangle * .5f)
                            {
                                if (stats.energy != 0)
                                {
                                    nav.speed = 0;
                                }
                                else
                                {
                                    nav.speed = speed;
                                }
                                nav.SetDestination(transform.position);
                            }
                            else
                            {
                                nav.speed = speed;
                            }

                            ischasing = true;
                            atplayer = true;
                            LastSighting = player.position;
                            if (Physics.Raycast(transform.position, direction.normalized, out hit, Meleedistance) && angle <= Meleeangle * .5f)
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
                                    targetpoint = player.position;
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
                            atplayer = false;
                            ischasing = true;
                            LastSighting = other.gameObject.transform.position;
                            if (Physics.Raycast(transform.position, direction.normalized, out hit, Meleedistance) && angle <= Meleeangle * .5f)
                            {
                                ismelee = true;
                                isshoot = false;
                                Destroy(hit.collider.gameObject);
                            }
                            else if (Physics.Raycast(transform.position, direction.normalized, out hit, shootdistance) && angle <= shootangle * .5f)
                            {
                              
                                if (Physics.Raycast(transform.position, direction.normalized, out hit, Meleedistance))
                                {
                                    ismelee = true;
                                    isshoot = false;
                                    Destroy(hit.collider.gameObject);
                                }
                                else
                                {
                                    isshoot = true;
                                    ismelee = false;
                                    targetpoint = player.position;
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
                        else if (hit.collider.gameObject.tag == ("ChasingGuard"))
                        {
                            if (ischasing == false)
                            {
                                LastSighting = hit.collider.gameObject.GetComponent<GuardAIv2>().LastSighting;
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
                                if (hit.collider.gameObject.GetComponent<CameraAI>().LastSighting != null)
                                {
                                    LastSighting = hit.collider.gameObject.GetComponent<CameraAI>().LastSighting;
                                }
                            }
                            C_Collider.enabled = true;
                            B_Collider.enabled = true;
                        }
                        else if (hit.collider.gameObject.tag == ("holoShield"))
                        {

                            ischasing = true;
                            LastSighting = other.gameObject.transform.position;
                        }
                        else
                        {

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
                else
                {

                    C_Collider.enabled = true;
                    B_Collider.enabled = true;
                }
            }
            else
            {
                C_Collider.enabled = true;
                B_Collider.enabled = true;
            }
        }
    }

    
    public void Shoot()
    {
        shotlight.enabled = true;
        shotParticles.Stop();
        shotParticles.Play();
        shotLine.enabled = true;
        shotLine.SetPosition(0, this.transform.position);
        shotLine.SetPosition(1, targetpoint);

        if (atplayer == true)
        {
            stats.energy = stats.energy - 30;
        }
        
    }
}
