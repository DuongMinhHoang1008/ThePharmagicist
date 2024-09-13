using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    private static PlayerInfo instance;
    private SlotClass[] inventoryItems;
    private PlayerInfo() {}
    public static PlayerInfo Instance() {
        if (instance == null) {
            instance = new PlayerInfo();
        }
        return instance;
    }
    public void UpdateGlobalInventory(ref SlotClass[] items) {
        if (inventoryItems == null) {
            inventoryItems = items;
        }
        items = inventoryItems;
    }
}
