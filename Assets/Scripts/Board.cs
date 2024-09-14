using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    BoardTile[] boardTileArr;
    Dictionary<Element, int> elementNumber;
    [SerializeField] BrewingInventoryManager brewingInventoryManager;
    [SerializeField] Sprite curePotionIcon;
    MagicIngredient magicIngredient = null;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        boardTileArr = new BoardTile[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            boardTileArr[i] = transform.GetChild(i).GetComponent<BoardTile>();
        }
        elementNumber = new Dictionary<Element, int>() {
            {Element.None, 0},
            {Element.Metal, 0},
            {Element.Water, 0},
            {Element.Wood, 0},
            {Element.Fire, 0},
            {Element.Earth, 0}
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CalculateElement() {
        foreach (BoardTile tile in boardTileArr) {
            if (tile.element != Element.None) {
                elementNumber[tile.element] += tile.value;
            }
        }
        string res = "";
        foreach (Element element in elementNumber.Keys) {
            res += element + ": " + elementNumber[element] + "\n";
        }
        BrewNewPotion();
        elementNumber[Element.Metal] = 0;
        elementNumber[Element.Water] = 0;
        elementNumber[Element.Wood] = 0;
        elementNumber[Element.Fire] = 0;
        elementNumber[Element.Earth] = 0;
        ClearBoard();
    }
    public void BrewNewPotion() {
        if (CanMakeMagicPotion()) {
            brewingInventoryManager.AddItem(magicIngredient.GetMagicPotion(), 1);
            brewingInventoryManager.CopyInventoryToTemp();
            magicIngredient = null;
        } else {
            CurePotionClass curePotionClass = ScriptableObject.CreateInstance<CurePotionClass>();
            string name = "CurePotionEl" 
                            + "M" + elementNumber[Element.Metal]
                            + "Wa" + elementNumber[Element.Water]
                            + "Wo" + elementNumber[Element.Wood]
                            + "F" + elementNumber[Element.Fire]
                            + "E" + elementNumber[Element.Earth]; 
            string path = "Assets/Prefab/Potion/CurePotion/" + name + ".asset";

            if (!System.IO.File.Exists(path)) {
                curePotionClass.SetElementValue(elementNumber[Element.Metal],
                                                elementNumber[Element.Water],
                                                elementNumber[Element.Wood],
                                                elementNumber[Element.Fire],
                                                elementNumber[Element.Earth],
                                                curePotionIcon);

                AssetDatabase.CreateAsset(curePotionClass, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            } else {
                curePotionClass = AssetDatabase.LoadAssetAtPath<CurePotionClass>(path);
            }

            brewingInventoryManager.AddItem(curePotionClass, 1);
            brewingInventoryManager.CopyInventoryToTemp();
        }
    }
    public void ClearBoard() {
        foreach (BoardTile boardTile in boardTileArr) {
            boardTile.ClearAll();
        }
        magicIngredient = null;
    }
    public void SetMagicIngredient(MagicIngredient ingredient) {
        magicIngredient = ingredient;
    }
    public MagicIngredient GetMagicIngredient() {
        return magicIngredient;
    }
    bool CanMakeMagicPotion() {
        if (magicIngredient != null) {
            MagicPotionClass magicPotion = magicIngredient.GetMagicPotion();
            if (magicPotion.metalValue != 0 && magicPotion.metalValue != elementNumber[Element.Metal]) {
                return false;
            }
            if (magicPotion.waterValue != 0 && magicPotion.waterValue != elementNumber[Element.Water]) {
                return false;
            }
            if (magicPotion.woodValue != 0 && magicPotion.woodValue != elementNumber[Element.Wood]) {
                return false;
            }
            if (magicPotion.fireValue != 0 && magicPotion.fireValue != elementNumber[Element.Fire]) {
                return false;
            }
            if (magicPotion.earthValue != 0 && magicPotion.earthValue != elementNumber[Element.Earth]) {
                return false;
            }
            return true;
        }
        return false;
    }
}
