using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour
{
    Text text;
    string newtext;
    void Start()
    {
        GetComponent<Stamina>();
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = Stamina.energy.ToString();
    }
}
