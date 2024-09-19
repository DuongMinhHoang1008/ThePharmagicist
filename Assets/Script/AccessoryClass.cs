using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Accessory",menuName ="Item/Accessory")]
public class AccessoryClass : ItemClass{
    [SerializeField] public float metalBuff;
    [SerializeField] public float waterBuff;
    [SerializeField] public float woodBuff;
    [SerializeField] public float fireBuff;
    [SerializeField] public float earthBuff;
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() { return null; }

    public override ConsumableClass GetConsumable() { return null; }
}
