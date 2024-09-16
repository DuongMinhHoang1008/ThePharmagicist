using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    private static PlayerInfo instance;
    private SlotClass[] inventoryItems;
    private Vector2 playerPos = Vector2.zero;
    public Element element { get; private set; } = Element.None;
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
    public void UpdatePlayerPos(Vector2 pos) {
        playerPos = pos;
    }
    public Vector2 GetPlayerPos() {
        return playerPos;
    }
    public void SetPlayerElement(Element element) {
        this.element = element;
    }
}
