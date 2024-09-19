using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Thirdweb;

public class InventoryManager : MonoBehaviour
{
    protected UIManager UIManager;
    [SerializeField] protected GameObject slotsHolder;
    [SerializeField] protected ItemClass itemToAdd;
    
    [SerializeField] protected ItemClass itemToRemove;
    [SerializeField] public SlotClass[] items;
    [SerializeField] protected SlotClass[] herb;
    [SerializeField] protected SlotClass[] potion;

    [SerializeField] protected SlotClass[] startingItems;

    [SerializeField] protected SlotClass movingSlot;//di chuyển các slot khác cho cái item
    [SerializeField] protected SlotClass originalSlot;
    [SerializeField] protected SlotClass tempSlot;

    [SerializeField] GameObject showItemPickup;

    public Image itemCursor;

    [SerializeField] public GameObject[] slots;
    [SerializeField] AudioSource audioSource;
    public bool isMoving;

    //[SerializeField] private List<SlotClass> items = new List<SlotClass>();
    private void Start()
    {
        

        UIManager = FindAnyObjectByType<UIManager>();
        slots = new GameObject[slotsHolder.transform.childCount];
        items = new SlotClass[slots.Length];
        potion = new SlotClass[slots.Length];
        herb = new SlotClass[slots.Length];


        for (int i = 0;i < slots.Length;i++)
        {
            slots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }

        for(int i = 0;i < slots.Length; i++)
        {
            items[i] = new SlotClass();
            herb[i] = new SlotClass();
            potion[i] = new SlotClass();
        }

        for(int i = 0; i < startingItems.Length; i++)
        {
            if(startingItems[i].GetQuantity() >= 1)
            {
                items[i] = startingItems[i];
            }
            
        }

        PlayerInfo.Instance().UpdateGlobalInventory(ref items);
        RefreshUI();
    }
    public void Classify()
    {
        RefreshUI();
        for(int i = 0; i < slots.Length; i++)
        {
            herb[i].RemoveItem();
            potion[i].RemoveItem();
        }
    }
    public void ClassifyPotion()
    {
        int j = 0;
        for (int i = 0; i < items.Length; i++)
        {
            var currentItem = items[i].GetItem();
            if ((currentItem is PotionClass))
            {
                potion[j].AddItem(items[i].GetItem(),items[i].GetQuantity());
                j++;
            }
        }
        j = 0;
        RefreshPotion();
        for (int i = 0; i < slots.Length; i++)
        {
            herb[i].RemoveItem();
            
        }
    }
    public void ClassifyHerb()
    {
        int j = 0;
        for (int i = 0; i < items.Length; i++)
        {
            var currentItem = items[i].GetItem();
            if ((currentItem is HerbClass))
            {
                herb[j].AddItem(items[i].GetItem(), items[i].GetQuantity());
                j++;
            }
        }
        j = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            
            potion[i].RemoveItem();
        }
        RefreshHerb();
    }
    private void Update()
    {

        
            
        if (Input.GetMouseButtonDown(0))
        {
            
            if (isMoving)
            {
                EndMove();
            }
            else
            {
                BeginMove();
            }
        }
        if (isMoving)
        {
            itemCursor.enabled = true;
            itemCursor.transform.position = Input.mousePosition;
            itemCursor.sprite = movingSlot.GetItem().itemIcon;
        }
        else
        {
            itemCursor.enabled = false;
            itemCursor.sprite = null;
        }
        
    }
    protected void RefreshHerb()
    {
            for (int i = 0; i < slots.Length; i++)
            {
                try
                {
                    slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = herb[i].GetItem().itemIcon;
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = herb[i].GetQuantity() + "";
                }
                catch
                {
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                    slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
    }
    protected void RefreshPotion()
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = potion[i].GetItem().itemIcon;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = potion[i].GetQuantity() + "";
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
    public void RefreshUI()
    {
        for (int i = 0;i<slots.Length;i++)
        {
            try
            {
                    slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity() + "";
                
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled =false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }

        }
        
    }

    // Update is called once per frame
    public void AddItem(ItemClass item,int quantity)
    {
        ShowItemRecieve(item.ItemName);
        SlotClass slot = ContainItem(item);
        if(slot != null)
        {
            slot.AddQuantity(quantity);
        }
        else
        {
            for(int i=0; i < items.Length; i++)
            {
                if(items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }
            
        }
        RefreshUI();
    }
    public void RemoveItem(ItemClass item,int quantity)
    {
        SlotClass temp = ContainItem(item);
        if (temp != null)
        {
            if(temp.GetQuantity() > 1)
            {
                temp.SubQuantity(quantity);
            }
            else
            {
                int slotToRemoveIndex = 0;
                SlotClass slotToRemove = new SlotClass();
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }

                items[slotToRemoveIndex].RemoveItem();
            }
        }
        else
        {
            return;
        }
        RefreshUI();
    }
    
    public SlotClass ContainItem(ItemClass item)
    {
        foreach(SlotClass slot in items)
        {
            if(slot != null && slot.GetItem() == item)
            {
                return slot;
                
            }
        }
        return null;
    }

    protected SlotClass GetCloseSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            
            if(Vector2.Distance(slots[i].transform.position,Input.mousePosition) <= 32)
            {
                return items[i];
            }
            else {}
        }
        return null;
    }
    
    protected void BeginMove()
    {
        originalSlot = GetCloseSlot();
        if(originalSlot == null || originalSlot.GetItem() == null)
        {
            return;
        }
        movingSlot.AddItem(originalSlot.GetItem(), originalSlot.GetQuantity());

        originalSlot.RemoveItem();
        isMoving = true;
        RefreshUI();
        return;
        
    }
    protected void EndMove()
    {
        originalSlot = GetCloseSlot();

        if(originalSlot == null)
        {
            AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.RemoveItem();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    tempSlot.AddItem(originalSlot.GetItem(), originalSlot.GetQuantity());
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity());
                    tempSlot.RemoveItem();

                    RefreshUI();
                    return;
                }
            }
            else
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.RemoveItem();
            }
        }

        
        isMoving = false;
        RefreshUI();
        return;
    }
   
    void ShowItemRecieve(string itemName) {
        if (showItemPickup != null) {
            audioSource.PlayOneShot(audioSource.clip, 1);
            GameObject showItem = Instantiate(showItemPickup, transform.position, Quaternion.identity);
            showItem.GetComponent<TextMeshPro>().text = itemName;
            showItem.GetComponent<TextMeshPro>().fontSize = 5;
            showItem.GetComponent<TextMeshPro>().color = Color.white;
            float randRad = UnityEngine.Random.Range(60, 120) * Mathf.Deg2Rad;
            Vector2 direc = new Vector2(Mathf.Cos(randRad), Mathf.Sin(randRad));
            showItem.GetComponent<Rigidbody2D>().AddForce(direc * UnityEngine.Random.Range(300, 400));
        }
    }
    public void ClaimNFT(ItemClass item) {
        if (item is MagicPotionClass) {
            Debug.Log(item.name);
            // BlockchainManager.Instance.ClaimNFT(item.name);
        } 
    }
}
