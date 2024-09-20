using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowingToken : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gold;
    [SerializeField] TextMeshProUGUI silv;
    // Start is called before the first frame update
    void Start()
    {
        gold.text = PlayerInfo.Instance().gold.ToString();
        silv.text = PlayerInfo.Instance().silv.ToString();
        Debug.Log(PlayerInfo.Instance().silv.ToString());
        Debug.Log(PlayerInfo.Instance());
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = PlayerInfo.Instance().gold.ToString();
        silv.text = PlayerInfo.Instance().silv.ToString();
    }
}
