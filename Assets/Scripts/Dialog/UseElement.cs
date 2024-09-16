using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class UseElement : MonoBehaviour
{
    public int[] lackElement;
    public int[] needElement;

    private static UseElement instance;
    private void Awake()
    {
        lackElement = new int[5];
        needElement = new int[5];
    }
    public static UseElement GetInstance()
    {
        return instance;
    }
}
