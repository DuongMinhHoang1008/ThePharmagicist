using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnitemManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    public InventoryManager inventoryManagers;
    public HerbClass herb;
    
    private PotionController potionControllerObject;
    public void Start()
    {
        potionControllerObject = FindObjectOfType<PotionController>();
        inventoryManager = FindObjectOfType<InventoryManager>();
        inventoryManagers = FindObjectOfType<InventoryManager>();
        //PotionController potionController = potionControllerObject.GetComponent<PotionController>();       
    }
    public void Loot()
    {
        for (int i = 0; i < inventoryManager.slots.Length; i++)
        {
            if (inventoryManager.items[i].GetItem() == null)
            {
               
                inventoryManager.AddItem(herb.GetHerb(), 1);
                inventoryManager.RefreshUI();
                
                break;
            }
        }
        Destroy(this.gameObject);
    }
    public void Setup(HerbClass herbs)
    {
        
            herb = herbs;
        
         // Gán dữ liệu đúng cho item hiện tại
    }
}
