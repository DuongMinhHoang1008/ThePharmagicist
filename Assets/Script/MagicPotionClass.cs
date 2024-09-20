using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New MagicPotion", menuName = "Item/MagicPotion")]
public class MagicPotionClass : CurePotionClass
{
    [SerializeField] Magic magic;
    public Magic GetMagic() { return magic; }
    override public string GetPotionName() {
        string name = ItemName + "\nCấp độ " + magic.level;
        return name;
    }
    override public string GetInfoPotion() {
        string info = "Nguyên tố: ";
        switch (magic.scriptableMagic.element) {
            case Element.Metal:
                info += "Kim";
                break;
            case Element.Water:
                info += "Thủy";
                break;
            case Element.Wood:
                info += "Mộc";
                break;
            case Element.Fire:
                info += "Hỏa";
                break;
            case Element.Earth:
                info += "Thổ";
                break;
            default:
                break;
        }
        int damage = (int) Math.Ceiling(magic.scriptableMagic.damage * Math.Pow(1.25f ,magic.level - 1));
        info += "\nSát thương: " + damage + "\n"
                + "Hồi chiêu: " + magic.scriptableMagic.cooldown + "s\n";
        
        return info;
    } 
    public string GetPotionReq() {
        return "Kim: " + metalValue + "\n"
                    + "Thủy: " + waterValue + "\n"
                    + "Mộc: " + woodValue + "\n"
                    + "Hỏa: " + fireValue + "\n"
                    + "Thổ: " + earthValue;
    }
}
