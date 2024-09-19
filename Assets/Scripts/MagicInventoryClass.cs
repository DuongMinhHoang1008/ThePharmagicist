using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagicInventoryClass : InventoryManager
{
    //For Magic
    [SerializeField] GameObject itemInfo;
    [SerializeField] GameObject equipButton;
    [SerializeField] GameObject firstMagicButton;
    [SerializeField] GameObject secondMagicButton;
    [SerializeField] LaunchingMagicManager playerMagicManager;
    SlotClass useItemInSlot;
    // For Magic

    //[SerializeField] private List<SlotClass> items = new List<SlotClass>();
    private void Awake()
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
        equipButton.GetComponent<Button>().onClick.AddListener(EquipItem);
        firstMagicButton.GetComponent<Button>().onClick.AddListener(ChangeFirstMagic);
        secondMagicButton.GetComponent<Button>().onClick.AddListener(ChangeSecondMagic);
    }
    private void Update()
    {
        if (Time.timeScale != 1) {
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
        
        //For Magic
        if (Input.GetMouseButtonDown(1)) {
            CloseItemInfo();
            if (Time.timeScale != 1) {
                SlotClass currentSlot = GetCloseSlot();
                if (!isMoving && currentSlot != null) {
                    ShowItemInfo(currentSlot);
                } 
            }
        }
        //For Magic
    }
    //For Magic
    void ShowItemInfo(SlotClass slot) {
        ItemClass itemClass = slot.GetItem();
        itemInfo.SetActive(true);
        itemInfo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemClass.ItemName;
        itemInfo.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemClass.GetDescription();
        if (itemClass is AccessoryClass) {
            if (equipButton != null) {
                equipButton.SetActive(true);
                useItemInSlot = slot;
            }
        } else if (itemClass is MagicPotionClass) {
            if (firstMagicButton != null) {
                firstMagicButton.SetActive(true);
                useItemInSlot = slot;
            }
            if (secondMagicButton != null) {
                secondMagicButton.SetActive(true);
                useItemInSlot = slot;
            }
        } else {
            useItemInSlot = null;
        }
    }
    void CloseItemInfo() {
        itemInfo.SetActive(false);
        equipButton.SetActive(false);
        firstMagicButton.SetActive(false);
        secondMagicButton.SetActive(false);
    }
    public void EquipItem() {
        ItemClass itemClass = useItemInSlot.GetItem();
        useItemInSlot.RemoveItem();
        if (itemClass is AccessoryClass) {
            AccessoryClass tempAcc = playerMagicManager.GetAccessory();
            if (tempAcc != null) {
                useItemInSlot.AddItem(tempAcc, 1);
            }
            playerMagicManager.ChangeAccessory((AccessoryClass) itemClass);
        }
        RefreshUI();
        CloseItemInfo();
    }
    public void ChangeFirstMagic() {
        ItemClass itemClass = useItemInSlot.GetItem();
        useItemInSlot.RemoveItem();
        if (itemClass is MagicPotionClass) {
            MagicPotionClass magicPotionClass = (MagicPotionClass) itemClass;
            playerMagicManager.ChangeFirstMagic(magicPotionClass.GetMagic());
        }
        RefreshUI();
        CloseItemInfo();
    }
    public void ChangeSecondMagic() {
        ItemClass itemClass = useItemInSlot.GetItem();
        useItemInSlot.RemoveItem();
        if (itemClass is MagicPotionClass) {
            MagicPotionClass magicPotionClass = (MagicPotionClass) itemClass;
            playerMagicManager.ChangeSecondMagic(magicPotionClass.GetMagic());
        }
        RefreshUI();
        CloseItemInfo();
    }
    //For Magic
}
