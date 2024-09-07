using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionController : MonoBehaviour
{
    
    public PotionClass data;
    private ItemClass item;
  

    private GameObject newObject;
    public GameObject myObject;

    [SerializeField] private InventoryManager inventoryManager;
    private SlotClass slotClass;

    
   
    void Start()
    {
        //Khởi tạo potition
        GameObject newObject = new GameObject(data.ItemName);
        var currentitem = data.ItemName;
        SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();

        CircleCollider2D collider2D = newObject.AddComponent<CircleCollider2D>();
        
        
        spriteRenderer.sprite = data.itemIcon;
        newObject.transform.position = this.transform.position;
        newObject.layer = 6;
        SpawnitemManager spawnItemManager = newObject.AddComponent<SpawnitemManager>();
        spawnItemManager.Setup(data);
        myObject = GameObject.Find(currentitem);
        

    }
    
   
}
