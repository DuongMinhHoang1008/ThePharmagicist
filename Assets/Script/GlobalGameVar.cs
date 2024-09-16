using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum StatusEffect {
    None,
    Burn,
    Poison,
    Stun
}

public enum Element {
    None,
    Metal,
    Water,
    Wood,
    Fire,
    Earth
}

public class ElementInfo {
    public Color color {get; private set;}
    //plus = sinh; element sinh plus
    public Element plus {get; private set;}
    //minus = khac; element khac minus
    public Element minus {get; private set;}
    public Sprite sprite {get; private set;}
    public ElementInfo(Color co, Element p, Element m, Sprite s) {
        color = co;
        plus = p;
        minus = m;
        sprite = s;
    }
}

public class GlobalGameVar
{
    private static GlobalGameVar instance;
    public float blockWidth {get; private set;}
    public Dictionary<Element, ElementInfo> elementDic {get; private set;}
    private GlobalGameVar() {}
    static Sprite[] elementSprites;
    public static GlobalGameVar Instance() {
        if (instance == null) {
            elementSprites = Resources.LoadAll<Sprite>("Generic Status Icons");
            instance = new GlobalGameVar{
                blockWidth = 2.0f,
                elementDic = new Dictionary<Element, ElementInfo>()
                {
                    { Element.None, new ElementInfo(Color.white, Element.None, Element.None, null) },
                    { Element.Metal, new ElementInfo(Color.gray, Element.Water, Element.Wood, elementSprites[28]) },
                    { Element.Water, new ElementInfo(Color.blue, Element.Wood, Element.Fire, elementSprites[38]) },
                    { Element.Wood, new ElementInfo(Color.green, Element.Fire, Element.Earth, elementSprites[42]) },
                    { Element.Fire, new ElementInfo(Color.red, Element.Earth, Element.Metal, elementSprites[27]) },
                    { Element.Earth, new ElementInfo(Color.yellow, Element.Metal, Element.Water, elementSprites[36]) }
                }
            };
        }
        return instance;
    }
}
