using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsText : MonoBehaviour
{
    
    public TextMeshProUGUI text;

    public GameManager gameManager;

    [TextArea] public string halfMessage1;
    [TextArea] public string halfMessage2;

    public void Refresh()
    {
        text.text = halfMessage1 + gameManager.XPGained + halfMessage2;
    }

}
