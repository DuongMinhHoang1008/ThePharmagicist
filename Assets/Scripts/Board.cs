using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    BoardTile[] boardTileArr;
    Dictionary<Element, int> elementNumber;
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
        Debug.Log(res);
        elementNumber[Element.Metal] = 0;
        elementNumber[Element.Water] = 0;
        elementNumber[Element.Wood] = 0;
        elementNumber[Element.Fire] = 0;
        elementNumber[Element.Earth] = 0;
    }
}
