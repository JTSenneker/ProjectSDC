using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Text playerPoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetPointText();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Skill Point"))
        {
            PointManager.points++;
            other.gameObject.SetActive(false);
            SetPointText();
        }

        if (other.gameObject.CompareTag("Shop"))
        {
            SceneManager.LoadScene(sceneName: "Shop");
        }
    }

    public void SetPointText()
    {
        playerPoints.text = "Skill Points: " + PointManager.points.ToString();
    }
}
