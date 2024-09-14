using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagicIngredient", menuName = "Item/MagicIngredient")]
public class MagicIngredient : HerbClass
{
    [SerializeField] MagicPotionClass magicPotion;
    public MagicPotionClass GetMagicPotion() { return magicPotion; }
}
