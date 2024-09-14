using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New MagicPotion", menuName = "Item/MagicPotion")]
public class MagicPotionClass : CurePotionClass
{
    [SerializeField] Magic magic;
    public Magic GetMagic() { return magic; }
}
