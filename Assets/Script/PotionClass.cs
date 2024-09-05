using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Potion")]

public class PotionClass : ItemClass
{
    [Header("Potion")]
    public int toolDamage;
    public int toolDurability;
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion(ItemClass itemClass) { return this; }
    public override HerbClass GetHerb(SlotClass slotClass) { return null; }

    public override ConsumableClass GetConsumable(ItemClass itemClass) { return null; }
}
