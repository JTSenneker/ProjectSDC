using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")SceneManager.LoadScene("Victory");
    }
}
