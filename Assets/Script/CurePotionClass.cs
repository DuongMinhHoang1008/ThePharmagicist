using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cure Potion", menuName = "Item/CurePotion")]
public class CurePotionClass : ItemClass
{
    [Header("CurePotion")]
    [Header("Metal")]
    [SerializeField] public int metalValue = 0;
    [Header("Water")]
    [SerializeField] public int waterValue = 0;
    [Header("Wood")]
    [SerializeField] public int woodValue = 0;
    [Header("Fire")]
    [SerializeField] public int fireValue = 0;
    [Header("Earth")]
    [SerializeField] public int earthValue = 0;
    
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() {return null;}
    public override ConsumableClass GetConsumable() { return null; }
    public void SetElementValue(int metal, int water, int wood, int fire, int earth, Sprite sprite) {
        metalValue = metal; waterValue = water; woodValue = wood; fireValue = fire; earthValue = earth; 
        string name = "Thuốc chữa bệnh\n" 
                        + "M" + metalValue
                        + "Wa" + waterValue
                        + "Wo" + woodValue
                        + "F" + fireValue
                        + "E" + earthValue;
        itemIcon = sprite;
        ItemName = name;
    }
    virtual public string GetPotionName() {
        string name = "Thuốc chữa bệnh\n" 
                        + "M" + metalValue
                        + "Wa" + waterValue
                        + "Wo" + woodValue
                        + "F" + fireValue
                        + "E" + earthValue;
                        ItemName = name;
        return name;
    }
    virtual public string GetInfoPotion() {
        string info = "Kim: " + metalValue + "   "
                    + "Thủy: " + waterValue + "\n"
                    + "Mộc: " + woodValue + "   "
                    + "Hỏa: " + fireValue + "\n"
                    + "Thổ: " + earthValue;
        return info;
    }
    public override string GetDescription()
    {
        return GetInfoPotion();
    }
}
