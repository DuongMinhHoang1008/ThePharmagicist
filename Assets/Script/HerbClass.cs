using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Herb", menuName = "Item/Herb")]

public class HerbClass : ItemClass
{
    [Header("Herb")]
    public string itemDescription;
    [SerializeField] protected Element element;
    [SerializeField] protected GameObject ingredientShape;
    [SerializeField] int level;
    
    public override ItemClass GetItems(SlotClass slotClass) { return this; }
    public override PotionClass GetPotion() { return null; }
    public override HerbClass GetHerb() {return this;}

    public override ConsumableClass GetConsumable() { return null; }
    public Element GetElement() { return element; }
    public GameObject GetIngredientShape() { return ingredientShape; }
    public override string GetDescription() { 
        string info = "Nguyên tố: ";
        switch (element) {
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
        info += "\nCấp độ: " + level;
        return info;
    }
}
