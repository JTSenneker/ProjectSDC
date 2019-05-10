using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoringSystem : MonoBehaviour
{
    public int ExitLevel;
    public string Nextlevel;

   void OnTriggerEnter(Collider other)
    {
       ExitLevel = ExitLevel += 1;
        Debug.Log(ExitLevel);
        Destroy(gameObject);
        GoToNextLevel();
    }




    public void GoToNextLevel()
    {
        if (ExitLevel >= 1)
        {
            SceneManager.LoadScene(Nextlevel);
        }
    }
}
