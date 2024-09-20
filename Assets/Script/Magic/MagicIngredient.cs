using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagicIngredient", menuName = "Item/MagicIngredient")]
public class MagicIngredient : HerbClass
{
    [SerializeField] MagicPotionClass magicPotion;
    public MagicPotionClass GetMagicPotion() { return magicPotion; }
    public override string GetDescription()
    {
        string info = "Ma thuật: " + magicPotion.GetMagic().scriptableMagic.magicName + "\nNguyên tố: ";
        switch (magicPotion.GetMagic().scriptableMagic.element) {
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
        info += "\nĐiều kiện: \n" + magicPotion.GetPotionReq();
        return info;
    }
}
