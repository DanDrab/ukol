using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public ScoreCounter scoreCounter;
    public string prefix = "score: ";
    public string postfix = "$";
    
    private void LateUpdate()
    {
        textMeshProUGUI.text = $"{prefix}{scoreCounter.score}{postfix}";
    }
}
