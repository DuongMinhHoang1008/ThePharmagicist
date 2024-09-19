using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJSON;
    public QuestPrefab questPrefab;

    public bool playerInRange { get; private set; }
    private void Awake()
    {
        playerInRange = false;
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null) { 
            Player player = other.GetComponent<Player>();
            playerInRange = true;

            Debug.Log("on player");

            DialogManager.GetInstance().EnterDialogMode(inkJSON);
            if (questPrefab != null) {
                Debug.Log(gameObject.transform.parent.GetChild(1).name);
                CurePotionClass cure = player.GetInventory().FindCurePotion(gameObject.transform.parent.GetChild(1).GetComponent<UseElement>());
                if (cure != null) {
                    player.GetInventory().RemoveItem(cure, 1);
                    questPrefab.UpdateQuestProgress();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            playerInRange = false;
            DialogManager.GetInstance().ExitDialogMode();
        }
    }
}
