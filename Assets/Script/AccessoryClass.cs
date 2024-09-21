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
    public override string GetDescription()
    {
        string info = "Tăng sát thương nguyên tố:\n"
                    + "Kim: " + (metalBuff + 1) * 100 + "%\n"
                    + "Thủy: " + (waterBuff + 1) * 100 + "%\n"
                    + "Mộc: " + (woodBuff + 1) * 100 + "%\n"
                    + "Hỏa: " + (fireBuff + 1) * 100 + "%\n"
                    + "Thổ: " + (earthBuff + 1) * 100 + "%";
        return info;
    }
}
