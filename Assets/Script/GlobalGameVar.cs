using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
    public ElementInfo(Color co, Element p, Element m) {
        color = co;
        plus = p;
        minus = m;
    }
}

public class GlobalGameVar
{
    private static GlobalGameVar instance;
    public float blockWidth {get; private set;}
    public Dictionary<Element, ElementInfo> elementDic;
    private GlobalGameVar() {}
    public static GlobalGameVar Instance() {
        if (instance == null) {
            instance = new GlobalGameVar{
                blockWidth = 2.0f,
                elementDic = new Dictionary<Element, ElementInfo>()
                {
                    { Element.None, new ElementInfo(Color.white, Element.None, Element.None) },
                    { Element.Metal, new ElementInfo(Color.gray, Element.Water, Element.Wood) },
                    { Element.Water, new ElementInfo(Color.blue, Element.Wood, Element.Fire) },
                    { Element.Wood, new ElementInfo(Color.green, Element.Fire, Element.Earth) },
                    { Element.Fire, new ElementInfo(Color.red, Element.Earth, Element.Metal) },
                    { Element.Earth, new ElementInfo(Color.yellow, Element.Metal, Element.Water) }
                }
            };
        }
        return instance;
    }
}
