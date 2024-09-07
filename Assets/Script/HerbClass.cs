using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Item/Herb")]

public class HerbClass : ItemClass
{
    [Header("Herb")]
    public string itemDescription;
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() {return this;}

    public override ConsumableClass GetConsumable() { return null; }
}
