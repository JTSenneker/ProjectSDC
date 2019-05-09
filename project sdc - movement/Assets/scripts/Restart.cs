using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public void Update()
    {
        if (Input.anyKeyDown)
        {
            LevelRestart();
        }
    }
    public void LevelRestart()
    {
        SceneManager.LoadScene("Demo Level");
    }
}
