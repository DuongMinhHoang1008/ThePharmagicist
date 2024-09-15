using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Dynamitey.Internal.Optimization;
using Thirdweb;
using Thirdweb.Contracts.DropERC1155.ContractDefinition;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlockchainManager : MonoBehaviour
{
    public UnityEvent<string> OnLoggedIn;
    public string Address { get; private set; }

    public static BlockchainManager Instance { get; private set; }
    public Button ClaimButton;
    public TextMeshPro goldText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public async void ClaimGold()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Debug.Log("Claiming Gold...");
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x837Da77508c6c131a7A67546a95AfB754e7Fb9b7");
        var result = await contract.ERC20.ClaimTo(Address, "10");

        Debug.Log("Clamed Gold: " + result);
    }
}