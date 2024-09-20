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
        gold.text = BlockchainManager.Instance.userBalance.gold;
        silv.text = BlockchainManager.Instance.userBalance.silver;
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = BlockchainManager.Instance.userBalance.gold;
        silv.text = BlockchainManager.Instance.userBalance.silver;
    }
}
