using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class altar : MonoBehaviour
{
    [SerializeField] private GameObject UI_NPC;
    [SerializeField] private GameObject showUI;
    [SerializeField] private GameObject showAI;

    [SerializeField] private Button button;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            UI_NPC.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        UI_NPC.SetActive(false);
        showUI.SetActive(false);
        showAI.SetActive(false);
    }
}
