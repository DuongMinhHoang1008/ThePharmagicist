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
    private string GOLD_CONTRACT_ADDRESS = "0xC56258A3ee06d48A5082735A88BC4Ace635E3fEA";
    private string SILVER_CONTRACT_ADDRESS = "0x898cA645E4D8E4c21cBAcf4EFBE7CdAB5345E791";
    private string NFT_CONTRACT_ADDRESS = "0xc1Ee09eD05A1fA7FaCf64356ba8dEACcDb348d21";
    Dictionary<string, string> NFTtoID = new Dictionary<string, string>()
{
    { "Thuoc_bach_kim_1", "1" },
    { "Thuoc_bach_kim_2", "2" },
    { "Thuoc_bach_kim_3", "3" },
    { "Thuoc_han_vu_1", "4" },
    { "Thuoc_han_vu_2", "5" },
    { "Thuoc_han_vu_3", "6" },
    { "tEST", "7" },
    { "Thuoc_hoa_diem_1", "8" },
    { "Thuoc_hoa_diem_2", "9" },
    { "Thuoc_hoa_diem_3", "10" },
    { "Thuoc_hoang_ha_1", "11" },
    { "Thuoc_hoang_ha_2", "12" },
    { "Thuoc_hoang_ha_3", "13" },
    { "Thuoc_hong_ha_1", "14" },
    { "Thuoc_hong_ha_2", "15" },
    { "Thuoc_hong_ha_3", "16" },
    { "Thuy_cau", "17" },
    { "Cau_lua", "18" },
    { "Gai_doc", "19" },
    { "Kim_tieu", "20" },
    { "Nam_dam_da", "21" },
    { "cay_bac_ha", "22" },
    { "cay_bach_bo", "23" },
    { "cay_ca_gai_leo", "24" },
    { "cay_dinh_lang", "25" },
    { "cay_gung", "26" },
    { "cay_hoa_cuc", "27" },
    { "cay_luoi_ran", "28" },
    { "cay_nghe", "29" },
    { "cay_sam", "30" },
    { "cay_xa", "31" },
    { "cu_gung", "32" },
    { "cu_nghe", "33" },
    { "hoa_cuc", "34" },
    { "la_bac_ha", "35" },
    { "la_dinh_lang", "36" },
    { "la_xa", "37" },
    { "re_bach_bo", "38" },
    { "re_cay_ca_gai", "39" },
    { "sam", "40" },
    { "Vong_co_thuy", "41" },
    { "Vong_co_hoa", "42" },
    { "Vong_co_kim", "43" },
    { "Vong_co_moc", "44" },
    { "Vong_co_ngu_hanh", "45"},
    { "Vong_co_tho", "46" },

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
        await contract_gold.ERC20.ClaimTo(Address, "50");
        Debug.Log("Claimed Gold");
    }

    public async void ClaimSilv(int level) { // easy - 1 , medium - 2 , hard - 3
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract_silv = ThirdwebManager.Instance.SDK.GetContract(SILVER_CONTRACT_ADDRESS);
        if (level == 1)
        {
            await contract_silv.ERC20.ClaimTo(Address, "50");
        }
        else if (level == 2)
        {
            await contract_silv.ERC20.ClaimTo(Address, "20");
        }
        else if (level == 3)
        {
            await contract_silv.ERC20.ClaimTo(Address, "30");
        }
        Debug.Log("Claimed Silver");
    }
    public async void ClaimNFT(string name) { 
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract = ThirdwebManager.Instance.SDK.GetContract(NFT_CONTRACT_ADDRESS);
        await contract.ERC1155.ClaimTo(Address, NFTtoID[name], 1);
        Debug.Log("Claimed NFT");
    }
    public async void ATransferNFT(string name, string toAddress, int amount) {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var contract = ThirdwebManager.Instance.SDK.GetContract(NFT_CONTRACT_ADDRESS);
        await contract.ERC1155.Transfer(toAddress, NFTtoID[name], amount);
        
    }
    public async void GetBalance()
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();

        var contract_gold = ThirdwebManager.Instance.SDK.GetContract(GOLD_CONTRACT_ADDRESS);
        var balanceGold = await contract_gold.ERC20.BalanceOf(Address);
        userBalance.gold = balanceGold.displayValue;
        Debug.Log("Gold: " + userBalance.gold);

        var contract_silv = ThirdwebManager.Instance.SDK.GetContract(SILVER_CONTRACT_ADDRESS);
        var balance_silv = await contract_silv.ERC20.BalanceOf(Address);
        userBalance.silver = balance_silv.displayValue;
        Debug.Log("Silver: " + userBalance.silver);
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


/*
Thuoc_bach_kim_1
Thuoc_bach_kim_2
Thuoc_han_vu_1
Thuoc_hoa_diem_1
cay_dinh_lang
cay_gung
sam
*/