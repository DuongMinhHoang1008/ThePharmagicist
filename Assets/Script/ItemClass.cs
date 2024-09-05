using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="New Item")]
public abstract class ItemClass :ScriptableObject
{
    public string ItemName;
    public Sprite itemIcon;
    public bool isStackable = true;
    public abstract ItemClass GetItems(SlotClass slotClass);
    public abstract PotionClass GetPotion(ItemClass itemClass);
    public abstract HerbClass GetHerb(SlotClass slotClass);

    public abstract ConsumableClass GetConsumable(ItemClass itemClass);



    
}
