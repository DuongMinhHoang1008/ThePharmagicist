using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Item/Herb")]

public class HerbClass : ItemClass
{
    [Header("Herb")]
    public string itemDescription;
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion(ItemClass itemClass) { return null; }
    public override HerbClass GetHerb(SlotClass slotClass) {return this;}

    public override ConsumableClass GetConsumable(ItemClass itemClass) { return null; }
}
