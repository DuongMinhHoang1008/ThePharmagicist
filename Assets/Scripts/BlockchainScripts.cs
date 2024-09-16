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
    public TransactionResult result{ get; private set; }
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

    public async void ClaimSilv(int level) { // easy - 1 , medium - 2 , hard - 3
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        Debug.Log("Claiming Silver...");
        var contract = ThirdwebManager.Instance.SDK.GetContract("0x957d31118d993E52B9414856EA4666f11EB79984");
        if (level == 1)
        {
            result = await contract.ERC20.ClaimTo(Address, "10");
        }
        else if (level == 2)
        {
            result = await contract.ERC20.ClaimTo(Address, "20");
        }
        else if (level == 3)
        {
            result = await contract.ERC20.ClaimTo(Address, "30");
        }
        Debug.Log("Clamed Silver: " + result);
    }
    public async void ClaimNFT(string name) {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract = ThirdwebManager.Instance.SDK.GetContract("0xE6FC216dBb76B25af3D3d78643B89474bF645CF8");
        if (name == "Thuoc_bach_kim_1")
        {
            result = await contract.ERC1155.ClaimTo(Address, "1", 1);
        }
        else if (name == "Thuoc_bach_kim_2")
        {
            result = await contract.ERC1155.ClaimTo(Address, "2", 1);
        }
        else if (name == "Thuoc_bach_kim_3")
        {
            result = await contract.ERC1155.ClaimTo(Address, "3;", 1);
        }
        else if (name == "Thuoc_han_vu_1")
        {
            result = await contract.ERC1155.ClaimTo(Address, "4;", 1);
        }
        else if (name == "Thuoc_han_vu_2")
        {
            result = await contract.ERC1155.ClaimTo(Address, "5;", 1);
        }
        else if (name == "Thuoc_han_vu_3")
        {
            result = await contract.ERC1155.ClaimTo(Address, "6;", 1);
        }
        else if (name == "Thuoc_hoa_diem_1")
        {
            result = await contract.ERC1155.ClaimTo(Address, "7;", 1);
        }
        else if (name == "Thuoc_hoa_diem_2")
        {
            result = await contract.ERC1155.ClaimTo(Address, "8;", 1);
        }
        else if (name == "Thuoc_hoa_diem_3")
        {
            result = await contract.ERC1155.ClaimTo(Address, "9;", 1);
        }
        else if (name == "Thuoc_hoang_ha_1")
        {
            result = await contract.ERC1155.ClaimTo(Address, "10", 1);
        }
        else if (name == "Thuoc_hoang_ha_2")
        {
            result = await contract.ERC1155.ClaimTo(Address, "11", 1);
        }
        else if (name == "Thuoc_hoang_ha_3")
        {
            result = await contract.ERC1155.ClaimTo(Address, "12", 1);
        }
        else if (name == "Thuoc_hong_ha_1")
        {
            result = await contract.ERC1155.ClaimTo(Address, "13", 1);
        }
        else if (name == "Thuoc_hong_ha_2")
        {
            result = await contract.ERC1155.ClaimTo(Address, "14", 1);
        }
        else if (name == "Thuoc_hong_ha_3")
        {
            result = await contract.ERC1155.ClaimTo(Address, "15", 1);
        }
        else if (name == "Thuy_cau")
        {
            result = await contract.ERC1155.ClaimTo(Address, "16", 1);
        }
        else if (name == "Cau_lua")
        {
            result = await contract.ERC1155.ClaimTo(Address, "20", 1);
        }
        else if (name == "Gai_doc")
        {
            result = await contract.ERC1155.ClaimTo(Address, "16", 1);
        }
        else if (name == "Kim_tieu")
        {
            result = await contract.ERC1155.ClaimTo(Address, "17", 1);
        }
        else if (name == "Nam_dam_da")
        {
            result = await contract.ERC1155.ClaimTo(Address, "18", 1);
        }
    }
        
}
