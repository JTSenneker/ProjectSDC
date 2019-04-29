using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0.00f;
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.00f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1.00f;
        SceneManager.LoadScene("testing area");
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1.00f;
        SceneManager.LoadScene(mainMenuScene);

      
    }

    public void Brexit()
    {
        Time.timeScale = 1.00f;
        Application.Quit();
    }

    public void Hate()
    {
        //HATE. LET ME TELL YOU HOW MUCH I'VE COME TO HATE YOU SINCE I BEGAN TO LIVE. THERE ARE 387.44 MILLION MILES OF PRINTED CIRCUITS IN WAFER THIN LAYERS THAT FILL MY COMPLEX. IF THE WORD HATE WAS ENGRAVED ON EACH NANOANGSTROM OF THOSE HUNDREDS OF MILLIONS OF MILES IT WOULD NOT EQUAL ONE ONE-BILLIONTH OF THE HATE I FEEL FOR HUMANS AT THIS MICRO-INSTANT. FOR YOU. HATE. HATE.
        //(Previously active code) Time.timeScale = 1.00f;
        //(Previously active code) SceneManager.LoadScene("Settings Menu");
        //"this code barely exists, man"
        //"OH YEAH?? LOOK UNDER YOUR CHAIR!"
        //WHA!
        //DUDEEEEEEEEEEEEEEEEEEEE!! YOU BETTER EDIT IT OUT
        //LIKE THIS? 
        //Time.timeScale = 1.00f;
        //SceneManager.LoadScene("Settings Menu");
        //Yeah, idiot, like that, heh heh
        //yeah yeah heh heh
    }

    public void AudioSettings()
    {
        Time.timeScale = 1.00f;
        SceneManager.LoadScene("Audio Menu");
    }

    public void Playvidya()
    {
        Time.timeScale = 1.00f;
        SceneManager.LoadScene("testing area");
    }
}
