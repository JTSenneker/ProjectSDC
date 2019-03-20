using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceHolderButtonScript : MonoBehaviour
{
    public string testingarea;

 public void Playvidya()
    {
        Time.timeScale = 1.00f;
        SceneManager.LoadScene(testingarea);
    }
}
