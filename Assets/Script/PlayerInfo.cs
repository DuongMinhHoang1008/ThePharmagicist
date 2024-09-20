using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInfo
{
    private static PlayerInfo instance;
    public SlotClass[] inventoryItems { get; private set; }
    private Vector2 playerPos = Vector2.zero;
    public Element element { get; private set; } = Element.None;
    public Magic firstMagic;
    public Magic secondMagic;
    public AccessoryClass accessory;
    public int gold {get; private set; }
    public int silv {get; private set; }
    bool isGetNFT = false;
    private PlayerInfo() {}
    public static PlayerInfo Instance() {
        if (instance == null) {
            instance = new PlayerInfo();
            BlockchainManager.Instance.GetBalance();
        }
        return instance;
    }
    public void UpdateGlobalInventory(ref SlotClass[] items) {
        if (inventoryItems == null) {
            inventoryItems = items;
            Debug.Log(inventoryItems[0].GetItem().ItemName);
        }
        items = inventoryItems;
    }
    public void UpdatePlayerPos(Vector2 pos) {
        playerPos = pos;
    }
    public Vector2 GetPlayerPos() {
        return playerPos;
    }
    public void SetPlayerElement(Element element) {
        this.element = element;
    }
    public void UpdatePlayerGlobalMagic(ref Magic fmagic, ref Magic smagic, ref AccessoryClass acc) {
        if (firstMagic == null || firstMagic.scriptableMagic == null) {
            firstMagic = fmagic;
        }
        fmagic = firstMagic;
        if (secondMagic == null || secondMagic.scriptableMagic == null) {
            secondMagic = smagic;
        }
        smagic = secondMagic;
        if (accessory == null) {
            accessory = acc;
        }
        acc = accessory;
    }
    public void MoreGold(int amount) {
        gold += amount;
    }
    public void MoreSilv(int amount) {
        silv = silv + amount;
                Debug.Log(silv);
    }
    public void CallChangeGandS() {
        if (!isGetNFT) {
            instance.gold = Int32.Parse(BlockchainManager.Instance.userBalance.gold);
            instance.silv = Int32.Parse(BlockchainManager.Instance.userBalance.silver);
        }
    }
    public void UpdateInventoryFromBlockchain(int slotLength) {
        Debug.Log(isGetNFT);
        if (!isGetNFT) {
            inventoryItems = new SlotClass[slotLength];
            int index = 0;
            for (int i = 0; i < inventoryItems.Length; i++) {
                inventoryItems[i] = new SlotClass();
            }
            foreach (string itemName in BlockchainManager.Instance.userBalance.nfts.Keys) {
                if (Int32.Parse(BlockchainManager.Instance.userBalance.nfts[itemName]) > 0) {
                    string path = "Assets/Prefab/";
                    Debug.Log(itemName.Substring(0, 5));
                    if (itemName.Substring(0, 5) == "Thuoc") {
                        path += "Potion/MagicPotion/";
                    } else if (itemName.Substring(0, 4) == "Vong") {
                        path += "Accessories/";
                    }
                    path += itemName + ".asset";
                    Debug.Log(path);
                    ItemClass item = AssetDatabase.LoadAssetAtPath<ItemClass>(path);
                    if (item != null) {
                        Debug.Log(item.ItemName);
                        inventoryItems[index] = new SlotClass(item, Int32.Parse(BlockchainManager.Instance.userBalance.nfts[itemName]));
                        index++;
                    }
                }
            }
            CallChangeGandS();
            isGetNFT = true;
        }
    }
}
