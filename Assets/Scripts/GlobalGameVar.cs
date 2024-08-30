using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Element {
    Metal,
    Water,
    Wood,
    Fire,
    Earth
}

public class GlobalGameVar
{
    private static GlobalGameVar instance;
    public float blockWidth {get; private set;}
    private GlobalGameVar() {}
    public static GlobalGameVar Instance() {
        if (instance == null) {
            instance = new GlobalGameVar();
            instance.blockWidth = 2.0f;
        }
        return instance;
    }
}
