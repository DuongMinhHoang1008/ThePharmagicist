using System.Collections;
using System.Collections.Generic;
using MetaMask.Editor.NaughtyAttributes;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] GameObject talk;
    [SerializeField] bool giveItem;
    [ShowIf("giveItem")] [SerializeField] ItemClass item;
    [ShowIf("giveItem")] [SerializeField] bool giveMagicPotion;
    [ShowIf("giveMagicPotion")] [SerializeField] MagicPotionClass[] allMagicPotion;
    // Start is called before the first frame update
    void Start()
    {
        talk.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (giveMagicPotion) {
            foreach (var potion in allMagicPotion) {
                if (PlayerInfo.Instance().element == potion.GetMagic().scriptableMagic.element) {
                    item = potion;
                    break;
                }
            }
            foreach (var slot in PlayerInfo.Instance().inventoryItems) {
                if (slot.GetItem() == item) {
                    item = null;
                    break;
                }
            }

            if (PlayerInfo.Instance().firstMagic.scriptableMagic != null || PlayerInfo.Instance().secondMagic.scriptableMagic != null) {
                item = null;
            }
        }
        if (other.GetComponent<Player>() != null) {
            talk.SetActive(true);
            if (giveItem && item != null) {
                other.GetComponent<Player>().GetInventory().AddItem(item, 1);
                other.GetComponent<Player>().GetInventory().ClaimNFT(item);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<Player>() != null) {
            talk.SetActive(false);
        }
    }
}
