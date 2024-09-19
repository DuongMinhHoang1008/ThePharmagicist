using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HerbDictionary
{
    public HerbClass herb;
    public HerbClass herbItem;
}

public class PotionController : MonoBehaviour
{
    
    [SerializeField] HerbDictionary[] herbDic;

    private GameObject newObject;
    public GameObject myObject;

    [SerializeField] private InventoryManager inventoryManager;
   
    private SlotClass slotClass;
    public int objectCount = 10;     
    public Vector3 spawnAreaMin;     
    public Vector3 spawnAreaMax;


    void Start()
    {
        //Khởi tạo
        for(int i = 0; i < herbDic.Length; i++)
        {
            // Tạo vị trí ngẫu nhiên trong giới hạn spawn
            Vector3 randomPosition = new Vector3(
            Random.Range(transform.position.x + spawnAreaMin.x, transform.position.x +  spawnAreaMax.x),
            Random.Range(transform.position.y + spawnAreaMin.y, transform.position.y + spawnAreaMax.y),
            Random.Range(transform.position.z + spawnAreaMin.z, transform.position.z + spawnAreaMax.z));
            GameObject newObject = new GameObject(herbDic[i].herb.ItemName);
            newObject.transform.parent = gameObject.transform;
            var currentitem = herbDic[i].herb.ItemName;
            newObject.transform.localScale = new Vector3(4f, 4f, 4f);
            SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
            CircleCollider2D collider2D = newObject.AddComponent<CircleCollider2D>();

            spriteRenderer.sprite = herbDic[i].herb.itemIcon;

            newObject.transform.position = randomPosition;
            newObject.layer = 6;
            SpawnitemManager spawnItemManager = newObject.AddComponent<SpawnitemManager>();

            spawnItemManager.Setup(herbDic[i].herbItem);
                    

            

        }
            
        
    }   
}
