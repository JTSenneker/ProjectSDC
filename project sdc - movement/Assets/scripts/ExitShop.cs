using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ExitShop : MonoBehaviour
{
    void OnClick()
    {
        SceneManager.LoadScene(sceneName: "testing area");
    }
}
