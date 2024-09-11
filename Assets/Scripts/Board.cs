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
    }
    public void BrewNewPotion() {
        CurePotionClass curePotionClass = ScriptableObject.CreateInstance<CurePotionClass>();
        string name = "CurePotionEl" 
                        + "M" + elementNumber[Element.Metal]
                        + "Wa" + elementNumber[Element.Water]
                        + "Wo" + elementNumber[Element.Wood]
                        + "F" + elementNumber[Element.Fire]
                        + "E" + elementNumber[Element.Earth]; 
        string path = "Assets/Prefab/Potion/CurePotion/" + name + ".asset";

        curePotionClass.SetElementValue(elementNumber[Element.Metal],
                                        elementNumber[Element.Water],
                                        elementNumber[Element.Wood],
                                        elementNumber[Element.Fire],
                                        elementNumber[Element.Earth],
                                        curePotionIcon);

        AssetDatabase.CreateAsset(curePotionClass, path);

        brewingInventoryManager.AddItem(curePotionClass, 1);
    }
}
