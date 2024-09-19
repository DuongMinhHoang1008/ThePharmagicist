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
        info += "\nSát thương: " + magic.scriptableMagic.damage + "\n"
                + "Hồi chiêu: " + magic.scriptableMagic.cooldown + "s\n";
        
        return info;
    } 
}
