using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    // Start is called before the first frame update
    public Element element {get; private set;}
    public int value {get; private set;}
    private void Awake() {
        element = Element.None;
    }
    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor(Color elementColor) {
        GetComponent<SpriteRenderer>().color = elementColor;
    }
    public void ChangeElement(Element el) {
        if (element == Element.None) {
            value = 1;
            element = el;
            GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[el].color;
        } else {
            if (element == GlobalGameVar.Instance().elementDic[el].plus) {
                value = 2;
                GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[element].color;
            } else if (el == GlobalGameVar.Instance().elementDic[element].plus) {
                value = 2;
                element = el;
                GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[el].color;
            } else if (el == GlobalGameVar.Instance().elementDic[element].minus
                || element == GlobalGameVar.Instance().elementDic[el].minus) {
                if (value == 2) {
                    value = 1;
                    GetComponent<SpriteRenderer>().color = GlobalGameVar.Instance().elementDic[element].color;
                } else {
                    value = 0;
                    element = Element.None;
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
    public bool CanBePlacedOn(Element blockEl) {
        if (blockEl != element) {
            return true;
        } else {
            return false;
        }
    }
    public void PlaceOn(Element el) {
        ChangeElement(el);
    }
}
