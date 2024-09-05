using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consum", menuName = "Item/Consumable")]

public class ConsumableClass : ItemClass
{
    [Header("Consumable")]
    public int healthRecovery;
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion(ItemClass itemClass) { return null; }
    public override HerbClass GetHerb(SlotClass slotClass) { return null; }

    public override ConsumableClass GetConsumable(ItemClass itemClass) { return this; }
}
