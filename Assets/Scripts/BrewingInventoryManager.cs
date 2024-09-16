using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BrewingInventoryManager : MonoBehaviour
{
    private UIManager UIManager;
    [SerializeField] private GameObject slotsHolder;
    [SerializeField] private ItemClass itemToAdd;
    
    [SerializeField] private ItemClass itemToRemove;
    [SerializeField] public SlotClass[] items;
    [SerializeField] private SlotClass[] herb;
    [SerializeField] private SlotClass[] potion;

    [SerializeField] private SlotClass[] startingItems;

    [SerializeField] private SlotClass movingSlot;//di chuyển các slot khác cho cái item
    [SerializeField] private SlotClass originalSlot;
    [SerializeField] private SlotClass tempSlot;
    [SerializeField] Board brewingBoard;
    [SerializeField] GameObject itemInfo;
    GameObject ingredientShape;

    public Image itemCursor;
    SlotClass[] tempItems;

    [SerializeField] public GameObject[] slots;
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
        tempItems = new SlotClass[items.Length];
        CopyInventory(items, ref tempItems);

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
            if (currentItem is HerbClass)
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
                GetItemToBrew();
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
    private void RefreshHerb()
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
    private void RefreshPotion()
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
            if(slot.GetItem() != null) {
            }
            if(slot != null && slot.GetItem() == item)
            {
                return slot;
                
            }
        }
        return null;
    }

    private SlotClass GetCloseSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            
            if(Vector2.Distance(slots[i].transform.position,Camera.main.ScreenToWorldPoint(Input.mousePosition)) <= 0.5f)
            {
                return items[i];
            }
            else {}
        }
        return null;
    }
    
    private void BeginMove()
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
    private void EndMove()
    {
        if(ingredientShape != null) {
            if(!ingredientShape.GetComponent<BlockGroup>().IsPlacable()) {
                Destroy(ingredientShape);
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

                RefreshUI();
            }
            isMoving = false;
        }
    }
   
    public void GetItemToBrew() {
        SlotClass currentSlot = GetCloseSlot();
        if(currentSlot == null || currentSlot.GetItem() == null)
        {
            CloseItemInfo();
            return;
        }
        HerbClass herbClass = currentSlot.GetItem().GetHerb();
        if (herbClass != null) {
            currentSlot.SubQuantity(1);
            isMoving = true;
            movingSlot.AddItem(currentSlot.GetItem(), 1);
            if (currentSlot.GetQuantity() == 0) {
                currentSlot.RemoveItem();
            }
            RefreshUI();

            ingredientShape = Instantiate(herbClass.GetIngredientShape(), Vector3.zero, Quaternion.identity);
            ingredientShape.GetComponent<BlockGroup>().SetElement(herbClass.GetElement());

            
            if (herbClass is MagicIngredient) {
                MagicIngredient magicIngredient = (MagicIngredient) herbClass;
                brewingBoard.SetMagicIngredient(magicIngredient);
            }
        } else {
            if (currentSlot.GetItem() is CurePotionClass) {
                ShowItemInfo((CurePotionClass) currentSlot.GetItem());
            }
        }
        return;
    }  
    public void UndoBrewing() {
        CopyInventory(tempItems, ref items);
        RefreshUI();
        brewingBoard.ClearBoard();
    }
    void CopyInventory(SlotClass[] from, ref SlotClass[] to) {
        for (int i = 0; i < from.Length; i++) {
            to[i] = new SlotClass(from[i].GetItem(), from[i].GetQuantity());
        }
    }
    public void CopyInventoryToTemp() {
        CopyInventory(items, ref tempItems);
    }
    void ShowItemInfo(CurePotionClass curePotion) {
        itemInfo.active = true;
        itemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = curePotion.GetPotionName();
        itemInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = curePotion.GetInfoPotion();
    }
    void CloseItemInfo() {
        itemInfo.active = false;
    }
}
