using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class UseElement : MonoBehaviour
{
    [Header("Metal")]
    [SerializeField] int metalValue = 0;
    [Header("Water")]
    [SerializeField] int waterValue = 0;
    [Header("Wood")]
    [SerializeField] int woodValue = 0;
    [Header("Fire")]
    [SerializeField] int fireValue = 0;
    [Header("Earth")]
    [SerializeField] int earthValue = 0;


    private static UseElement instance;
    public static UseElement GetInstance()
    {
        return instance;
    }
    public string GetInfoSick()
    {
        string info = "Kim: " + metalValue + "\n"
                    + "Thủy: " + waterValue + "\n"
                    + "Mộc: " + woodValue + "\n"
                    + "Hỏa: " + fireValue + "\n"
                    + "Thổ: " + earthValue;
        return info;
    }
    bool CheckUseElement(string curePotion)
    {
        if (GetInfoSick() == curePotion) return true;
        else return false;
    }
    private void Update()
    {
        
    }

}