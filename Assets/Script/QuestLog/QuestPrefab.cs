using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPrefab : MonoBehaviour
{
    public Button button { get;  set; }
    public TextMeshProUGUI buttonText;
    public int questCollected { get;  set; }
    public int questColletion { get;  set; }

    public static QuestPrefab instance { get; set; }
    public void Initialize(string displayName, int questCollected = 0, int questColletion = 0) 
    {
        this.questCollected = questCollected;
        this.questColletion = questColletion;
        this.button = this.GetComponent<Button>();
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        this.buttonText.text = displayName;
    }

}
