
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;
    PlayerStats playerStats;
    PlayerMovement playerMovement;
    public Transform player;
    public Transform target;
    public Transform hologramSpawnPoint;
    public bool HologrameMovement;
    public bool hologramInScene;
    public float aimSpeed;
    public float maxDistance;
    public float minDistance;
    public float hologramDepletion;
    public float hologramTimeToLive;
    public float timer;
    public Material toClose;
    public Material youCanShootThere;
    public GameObject hologramPrefab;
    void Start()
    {
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled == true)
        {
            HologrameMovement = true;
            //if (Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("w"))
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");
                move(h, v);
            }
        }
        else
        {
            HologrameMovement = false;
        }
        //activate hologram
        timer += Time.deltaTime;
        if (timer > hologramTimeToLive + 3)
        {
            timer = 0;
        }
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(player.position, (target.position - player.position));
        Physics.Raycast(player.position, (target.position - player.position), out hit);
        if (Vector3.Distance(player.position, target.position) > minDistance && Vector3.Distance(player.position, target.position) < maxDistance && hit.rigidbody == GameObject.Find("Hologram"))
        {
            gameObject.GetComponent<Renderer>().sharedMaterial = youCanShootThere;
            if (Input.GetButtonDown("hologram") && hologramInScene == false && GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled == true)
            {
                hologramInScene = true;
                playerStats.regenStamina = false;
                playerStats.TimerReset();
                playerStats.energy -= hologramDepletion;
                timer = 0;
                playerMovement.active = false;
                //GameObject.Find("HolographicPlayer").GetComponent<MeshRenderer>().enabled = true;
                //GameObject.Find("HolographicPlayer").GetComponent<BoxCollider>().enabled = true;
                Instantiate(hologramPrefab, hologramSpawnPoint.position, Quaternion.identity);
                GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled = false;
                //playerStats.Invoke("TimerReset", playerStats.regainDelay);
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().sharedMaterial = toClose;
        }
        if (timer > hologramTimeToLive)
        {
            //GameObject.Find("HolographicPlayer").GetComponent<MeshRenderer>().enabled = false;
            //GameObject.Find("HolographicPlayer").GetComponent<BoxCollider>().enabled = false;
            Object.Destroy(GameObject.Find("Player Hologram(Clone)"));
            hologramInScene = false;
        }
    }
    void move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * aimSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}


