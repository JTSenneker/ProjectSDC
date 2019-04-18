
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramController : MonoBehaviour
{
    Vector3 movement;
    Rigidbody rb;
    public Transform player;
    public Transform target;
    public bool HologrameMovement;
    public float aimSpeed;
    public float maxDistance;
    public float minDistance;
    private float currentDistance;
    void Start()
    {
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
        if (Vector3.Distance(player.position, target.position) > minDistance && Vector3.Distance(player.position, target.position) < maxDistance && hit.rigidbody == GameObject.Find("Hologram"))
        {
            Debug.Log("you are able to fire there.");
        }
        else
        {
            Debug.Log("you are not able to fire there");
        }
    }
}


