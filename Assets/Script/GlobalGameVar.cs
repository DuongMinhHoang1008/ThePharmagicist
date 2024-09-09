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
    private GlobalGameVar() {}
    public static GlobalGameVar Instance() {
        if (instance == null) {
            instance = new GlobalGameVar();
        }
        return instance;
    }
}
