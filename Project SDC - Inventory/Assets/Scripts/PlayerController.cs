using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public GameObject inventoryMenu;
    public bool inventoryOpen = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inventoryMenu.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryOpen == false)
            {
                inventoryMenu.gameObject.SetActive(true);
                inventoryOpen = true;
            }

            else if (inventoryOpen == true)
            {
                inventoryMenu.gameObject.SetActive(false);
                inventoryOpen = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shop"))
        {
            SceneManager.LoadScene(sceneName: "Shop");
        }
    }
}
