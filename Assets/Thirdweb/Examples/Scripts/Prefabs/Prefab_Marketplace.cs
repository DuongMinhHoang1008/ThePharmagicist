using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Org.BouncyCastle.Math.Field;
using UnityEngine;

namespace Thirdweb.Examples
{
    public class Prefab_Marketplace : MonoBehaviour
    {
        private const string TOKEN_ERC20_CONTRACT = "0x1738247AE53268d2B8C860A372D041B17653D28c";
        private const string DROP_ERC20_CONTRACT_GOLD = "0x837Da77508c6c131a7A67546a95AfB754e7Fb9b7";
        private const string DROP_ERC20_CONTRACT_SILV = "0x957d31118d993E52B9414856EA4666f11EB79984";
         private const string TOKEN_ERC721_CONTRACT = "0x345E7B4CCA26725197f1Bed802A05691D8EF7770";
        // private const string DROP_ERC721_CONTRACT = "0x6170158C1e6C6cd91091b4dcF84F6ae3a75A7707";
        private const string DROP_ERC1155_CONTRACT = "0xE6FC216dBb76B25af3D3d78643B89474bF645CF8";
        // private const string PACK_CONTRACT = "0xE33653ce510Ee767d8824b5EcDeD27125D49889D";
        private const string MARKETPLACE_CONTRACT = "0x450667c45F79C6a936d058c2BD044CB6aF8e11A5";
        private const string TOKEN_ERC1155_CONTRACT = "0xDf1020aC916d9B6136579218f64a30Cf05728ACD";
        // Fetching     
        public async void Fetch_DirectListing()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                var contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var marketplace = contract.Marketplace;
                var result = await marketplace.DirectListings.GetAll();
                Debugger.Instance.Log("[Fetch_DirectListing] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Fetch_DirectListing] Error", e.Message);
            }
        }

        public async void Fetch_Auction()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                var contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.GetAuction("0");
                Debugger.Instance.Log("[Fetch_Auction] Sucess", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Fetch_Auction] Error", e.Message);
            }
        }

        public async void Fetch_Offer()
        { 
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                var contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.GetOffer("0");
                Debugger.Instance.Log("[Fetch_Offer] Sucess", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Fetch_Offer] Error", e.Message);
            }
        }

        // Creating

        public async void Create_Listing()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.DirectListings.CreateListing(
                    new CreateListingInput()
                    {
                        assetContractAddress = TOKEN_ERC1155_CONTRACT,
                        tokenId = "0",
                        pricePerToken = "0.000000000000000001", // 1 wei
                        quantity = "100"
                    }
                );
                Debugger.Instance.Log("[Create_Listing] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Create_Listing] Error", e.Message);
            }
        }

        public async void Create_Auction()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.CreateAuction(
                    new CreateAuctionInput()
                    {
                        assetContractAddress = TOKEN_ERC1155_CONTRACT,
                        tokenId = "0",
                        buyoutBidAmount = "0.0000000000000001",
                        minimumBidAmount = "0.000000000000000001"
                    }
                );
                Debugger.Instance.Log("[Create_Auction] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Create_Auction] Error", e.Message);
            }
        }

        public async void Make_Offer()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.MakeOffer(
                    new MakeOfferInput()
                    {
                        assetContractAddress = TOKEN_ERC1155_CONTRACT,
                        tokenId = "0",
                        totalPrice = "0.000000000000000001",
                    }
                );
                Debugger.Instance.Log("[Make_Offer] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Make_Offer] Error", e.Message);
            }
        }

        // Closing

        public async void Buy_Listing()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.DirectListings.BuyFromListing("2", "1", await ThirdwebManager.Instance.SDK.Wallet.GetAddress());
                Debugger.Instance.Log("[Buy_Listing] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Buy_Listing] Error", e.Message);
            }
        }

        public async void Buyout_Auction()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.BuyoutAuction("0");
                Debugger.Instance.Log("[Buyout_Auction] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Buyout_Auction] Error", e.Message);
            }
        }

        public async void Accept_Offer()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.AcceptOffer("0");
                Debugger.Instance.Log("[Accept_Offer] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Accept_Offer] Error", e.Message);
            }
        }

        // Cancelling

        public async void Cancel_Listing()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.DirectListings.CancelListing("2");
                Debugger.Instance.Log("[Cancel_Listing] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Cancel_Listing] Error", e.Message);
            }
        }

        public async void Cancel_Auction()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.CancelAuction("0");
                Debugger.Instance.Log("[Cancel_Auction] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Cancel_Auction] Error", e.Message);
            }
        }

        public async void Cancel_Offer()
        {
            try
            {
                Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.CancelOffer("0");
                Debugger.Instance.Log("[Cancel_Offer] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                Debugger.Instance.Log("[Cancel_Offer] Error", e.Message);
            }
        }
        
    }
}
