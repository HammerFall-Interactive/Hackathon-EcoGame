using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class testtest : MonoBehaviour
{
    public TextMeshProUGUI gameStatusText;
    void Start()
    {
        bool ToolRDY = PlayerPrefs.GetInt("ToolRDY", 0) == 1;
        if (ToolRDY)
        {
            gameStatusText.text = "IT WORKS!!!";
        }
    }
    void Update()
    {
        

    }
}
