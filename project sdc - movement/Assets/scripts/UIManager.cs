using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image Staminabar;

    public void UpdateStaminaBar(float currentstamina, float maxstamina)
    {
        Staminabar.fillAmount = currentstamina / maxstamina;

    }



  
}
