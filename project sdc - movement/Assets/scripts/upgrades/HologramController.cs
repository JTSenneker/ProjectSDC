
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;
    PlayerStats playerStats;
    public Transform player;
    public Transform target;
    public bool HologrameMovement;
    public float aimSpeed;
    public float maxDistance;
    public float minDistance;
    public float hologramDepletion;
    public Material toClose;
    public Material youCanShootThere;
    void Start()
    {
        playerStats = GameObject.Find("player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (GameObject.Find("HologramTarget").GetComponent<MeshRenderer>().enabled == true)
        {
            HologrameMovement = true;
            if (Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("w"))
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
    }
    void move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * aimSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
    void FixedUpdate()
    {
        RaycastHit hit = new RaycastHit();
        Debug.DrawRay(player.position, (target.position - player.position));
        Physics.Raycast(player.position, (target.position - player.position),out hit);
        if (Vector3.Distance(player.position, target.position) > minDistance && Vector3.Distance(player.position, target.position) < maxDistance && hit.rigidbody == GameObject.Find("Hologram"))
        {
            gameObject.GetComponent<Renderer>().sharedMaterial = youCanShootThere;
            if (Input.GetKeyDown("space"))
            {
                playerStats.energy -= hologramDepletion;
                GameObject.Find("HolographicPlayer").GetComponent<MeshRenderer>().enabled = true;
                GameObject.Find("HolographicPlayer").GetComponent<BoxCollider>().enabled = true;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().sharedMaterial = toClose;
        }
    }
}


