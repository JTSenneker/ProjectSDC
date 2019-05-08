using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    PlayerStats playerStats;
    Vector3 movement;
    public float speed;
    public float shieldDepletion;
    private Rigidbody rb;
    private Transform shieldPoint;
    void Start()
    {
        playerStats =GameObject.Find("player").GetComponent<PlayerStats>();
        shieldPoint = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        transform.position = shieldPoint.position;
        //if (Input.GetKey("j") || Input.GetKey("k") || Input.GetKey("l") || Input.GetKey("i"))
        {
            float h = Input.GetAxisRaw("horizontal");
            float v = Input.GetAxisRaw("vertical");
            float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
        if(GameObject.Find("Shield").GetComponent<MeshRenderer>().enabled == true)
        {
            playerStats.energy -= shieldDepletion * Time.deltaTime;
            if (playerStats.energy < 30)
            {
                playerStats.energy = 30;
                playerStats.TimerReset();
            }
        }
    }
}
