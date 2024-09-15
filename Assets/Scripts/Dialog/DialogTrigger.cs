using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;

    public bool playerInRange { get; private set; }
    private void Awake()
    {
        playerInRange = false;
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") { 
            playerInRange = true;

            DialogManager.GetInstance().EnterDialogMode(inkJSON);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
            DialogManager.GetInstance().ExitDialogMode();
        }
    }
}
