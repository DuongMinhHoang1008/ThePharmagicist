using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Dynamitey.Internal.Optimization;
using Thirdweb;
using Thirdweb.Contracts.DropERC1155.ContractDefinition;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BlockchainManager : MonoBehaviour
{
    public UnityEvent<string> OnLoggedIn;
    public string Address { get; private set; }
    public TransactionResult result{ get; private set; }
    public static BlockchainManager Instance { get; private set; }
    public UserBalance userBalance = new UserBalance();
    private string GOLD_CONTRACT_ADDRESS = "0x837Da77508c6c131a7A67546a95AfB754e7Fb9b7";
    private string SILVER_CONTRACT_ADDRESS = "0x957d31118d993E52B9414856EA4666f11EB79984";
    private string NFT_CONTRACT_ADDRESS = "0xE6FC216dBb76B25af3D3d78643B89474bF645CF8";
    Dictionary<string, string> NFTtoID = new Dictionary<string, string>()
        {
            { "Thuoc_bach_kim_1", "1" },
            { "Thuoc_bach_kim_2", "2" },
            { "Thuoc_bach_kim_3", "3" },
            { "Thuoc_han_vu_1", "4" },
            { "Thuoc_han_vu_2", "5" },
            { "Thuoc_han_vu_3", "6" },
            { "Thuoc_hoa_diem_1", "7" },
            { "Thuoc_hoa_diem_2", "8" },
            { "Thuoc_hoa_diem_3", "9" },
            { "Thuoc_hoang_ha_1", "10" },
            { "Thuoc_hoang_ha_2", "11" },
            { "Thuoc_hoang_ha_3", "12" },
            { "Thuoc_hong_ha_1", "13" },
            { "Thuoc_hong_ha_2", "14" },
            { "Thuoc_hong_ha_3", "15" },
            { "Thuy_cau", "19" },
            { "Cau_lua", "20" },
            { "Gai_doc", "16" },
            { "Kim_tieu", "17" },
            { "Nam_dam_da", "18" },
            { "Vong_co_kim", "19" },
            { "Vong_co_thuy", "20" },
            { "Vong_co_moc", "21" },
            { "Vong_co_hoa", "22" },
            { "Vong_co_tho", "23" },
            { "Vong_co_ngu_hanh", "24" }
        };

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
        // userBalance = new UserBalance();
        userBalance.nfts = new Dictionary<string, string>();
    }
    public async void ClaimGold()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract_gold = ThirdwebManager.Instance.SDK.GetContract(GOLD_CONTRACT_ADDRESS);
        await contract_gold.ERC20.ClaimTo(Address, "10");
    }

    public async void ClaimSilv(int level) { // easy - 1 , medium - 2 , hard - 3
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract_silv = ThirdwebManager.Instance.SDK.GetContract(SILVER_CONTRACT_ADDRESS);
        if (level == 1)
        {
            await contract_silv.ERC20.ClaimTo(Address, "10");
        }
        else if (level == 2)
        {
            await contract_silv.ERC20.ClaimTo(Address, "20");
        }
        else if (level == 3)
        {
            await contract_silv.ERC20.ClaimTo(Address, "30");
        }
    }
    public async void ClaimNFT(string name) { 
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract = ThirdwebManager.Instance.SDK.GetContract(NFT_CONTRACT_ADDRESS);
        await contract.ERC1155.ClaimTo(Address, NFTtoID[name], 1);
    }
    public async void GetBalance()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract_gold = ThirdwebManager.Instance.SDK.GetContract(GOLD_CONTRACT_ADDRESS);
        var balanceGold = await contract_gold.ERC20.BalanceOf(Address);
        userBalance.gold = balanceGold.displayValue;

        var contract_silv = ThirdwebManager.Instance.SDK.GetContract(SILVER_CONTRACT_ADDRESS);
        var balance_silv = await contract_silv.ERC20.BalanceOf(Address);
        userBalance.silver = balance_silv.displayValue;

        var contract_nft = ThirdwebManager.Instance.SDK.GetContract(NFT_CONTRACT_ADDRESS);
        foreach (var nft in NFTtoID)
        {
            var name = nft.Key;
            var balance = await contract_nft.ERC1155.BalanceOf(Address, nft.Value);
            Debug.Log(name + " : " + balance);
            if (userBalance.nfts.ContainsKey(name)) {
                userBalance.nfts[name] = balance.ToString();
            }
            else
            {
                userBalance.nfts.Add(name, balance.ToString());
            };
        }
    }
}

public struct UserBalance {
    public string gold;
    public string silver;
    public Dictionary<string, string> nfts;
}