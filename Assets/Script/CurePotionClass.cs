using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cure Potion", menuName = "Item/CurePotion")]
public class CurePotionClass : ItemClass
{
    [Header("CurePotion")]
    [Header("Metal")]
    [SerializeField] int metalValue = 0;
    [Header("Water")]
    [SerializeField] int waterValue = 0;
    [Header("Wood")]
    [SerializeField] int woodValue = 0;
    [Header("Fire")]
    [SerializeField] int fireValue = 0;
    [Header("Earth")]
    [SerializeField] int earthValue = 0;
    
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() {return null;}
    public override ConsumableClass GetConsumable() { return null; }
    public void SetElementValue(int metal, int water, int wood, int fire, int earth, Sprite sprite) {
        metalValue = metal; waterValue = water; woodValue = wood; fireValue = fire; earthValue = earth; 
        itemIcon = sprite;
    }
}
