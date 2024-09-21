using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Org.BouncyCastle.Math.Field;
using UnityEngine;
using ZXing.Client.Result;
using System.Numerics;
namespace Thirdweb.Examples
{
    public class Prefab_Marketplace : MonoBehaviour
    {
        private const string TOKEN_ERC20_CONTRACT = "0x181FCa68CD8145F2f287bC34546542c0c3f15ad7";
        // private const string DROP_ERC20_CONTRACT_GOLD = "0x837Da77508c6c131a7A67546a95AfB754e7Fb9b7";
        // private const string DROP_ERC20_CONTRACT_SILV = "0x957d31118d993E52B9414856EA4666f11EB79984";
        //  private const string TOKEN_ERC721_CONTRACT = "0x22A5c81399224Fd55EA33B9a43f0a8333fBfF544";
        // private const string DROP_ERC721_CONTRACT = "0x6170158C1e6C6cd91091b4dcF84F6ae3a75A7707";
        // private const string PACK_CONTRACT = "0xE33653ce510Ee767d8824b5EcDeD27125D49889D";
        //private const string TOKEN_ERC1155_CONTRACT = "0xDf1020aC916d9B6136579218f64a30Cf05728ACD";
        private const string MARKETPLACE_CONTRACT = "0xe1aa5ed064D53e9Cf94aaef5190eDf00a50fD003";
        private const string DROP_ERC1155_CONTRACT = "0xc1Ee09eD05A1fA7FaCf64356ba8dEACcDb348d21";
        private const string SILV_ADDRESS = "0x898cA645E4D8E4c21cBAcf4EFBE7CdAB5345E791";
        private const string GOLD_ADDRESS = "0xC56258A3ee06d48A5082735A88BC4Ace635E3fEA";

        // Fetching     
        public async void Fetch_DirectListing()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                var contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                Debug.Log("Get Contract - Success");

                Marketplace marketplace = contract.Marketplace;
                Debug.Log(marketplace);
                
                var result = await marketplace.DirectListings.GetAll();
                foreach (var item in result)
                {
                    Debug.Log(item);
                }
                Debug.Log(marketplace.DirectListings);
                Debug.Log(result);
                //Debugger.Instance.Log("[Fetch_DirectListing] Success", result.ToString());

            }
            catch (System.Exception e)
            {
               Debug.Log(e);
            }
        }

        public async void Fetch_Auction()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                var contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.GetAuction("0");
                //Debugger.Instance.Log("[Fetch_Auction] Sucess", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Fetch_Auction] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        public async void Fetch_Offer()
        { 
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                var contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.GetOffer("0");
                //Debugger.Instance.Log("[Fetch_Offer] Sucess", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Fetch_Offer] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        // Creating

        public async void Create_Listing(int tokenId)
        {
            try
            {   
                Debug.Log("Create Listing - Start. ID: " + tokenId);
                Contract nftContract = ThirdwebManager.Instance.SDK.GetContract(DROP_ERC1155_CONTRACT);
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                TransactionResult trans = await nftContract.Write("setApprovalForAll", MARKETPLACE_CONTRACT, true);
                Debug.Log("ApprovalForAll - Success");
                string currencyContract;
                if (tokenId >= 22 && tokenId <= 40)
                {
                    currencyContract = SILV_ADDRESS;
                }
                else
                {
                    currencyContract = GOLD_ADDRESS;
                }
                var result = await contract.Marketplace.DirectListings.CreateListing(
                    new CreateListingInput()
                    {
                        currencyContractAddress = currencyContract,
                        assetContractAddress = DROP_ERC1155_CONTRACT,
                        tokenId = tokenId.ToString(),
                        quantity = "1",
                        pricePerToken = "30"
                    }
                );
                Debug.Log(result);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }

        public async void Create_Auction()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.CreateAuction(
                    new CreateAuctionInput()
                    {
                        assetContractAddress = DROP_ERC1155_CONTRACT,
                        tokenId = "0",
                        buyoutBidAmount = "0.0000000000000001",
                        minimumBidAmount = "0.000000000000000001"
                    }
                );
                //Debugger.Instance.Log("[Create_Auction] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Create_Auction] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        public async void Make_Offer()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.MakeOffer(
                    new MakeOfferInput()
                    {
                        assetContractAddress = DROP_ERC1155_CONTRACT,
                        tokenId = "0",
                        totalPrice = "0.000000000000000001",
                    }
                );
                //Debugger.Instance.Log("[Make_Offer] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Make_Offer] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        // Closing

                public async void Buy_Listing(string listingId)
        {
            try
            {
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                Marketplace marketplace = contract.Marketplace;
                var Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
                var listing = await marketplace.DirectListings.GetListing(listingId);
                var pricePerToken = BigInteger.Parse(listing.pricePerToken);
                var contractERC20 = ThirdwebManager.Instance.SDK.GetContract(TOKEN_ERC20_CONTRACT);
                var contractSILV = ThirdwebManager.Instance.SDK.GetContract(SILV_ADDRESS);
                var contractGOLD = ThirdwebManager.Instance.SDK.GetContract(GOLD_ADDRESS);
                BigInteger result;
                var currencyContract = listing.currencyContractAddress;
                if (currencyContract == SILV_ADDRESS)
                {
                    result = await contractSILV.Read<BigInteger>("allowance", Address, MARKETPLACE_CONTRACT);
                    if (result < BigInteger.Parse(listing.pricePerToken)) {
                    await contractSILV.Write("approve", MARKETPLACE_CONTRACT, pricePerToken - result);
                    }    
                }
                else if (currencyContract == GOLD_ADDRESS)
                {
                    result = await contractGOLD.Read<BigInteger>("allowance", Address, MARKETPLACE_CONTRACT);
                    if (result < BigInteger.Parse(listing.pricePerToken)) {
                    await contractGOLD.Write("approve", MARKETPLACE_CONTRACT, pricePerToken - result);
                    }   
                }
                else
                {
                    result = await contractERC20.Read<BigInteger>("allowance", Address, MARKETPLACE_CONTRACT);
                    if (result < BigInteger.Parse(listing.pricePerToken)) {
                    await contractERC20.Write("approve", MARKETPLACE_CONTRACT, pricePerToken - result);
                    }
                }
                
                await marketplace.DirectListings.BuyFromListing(listingId, "1", Address);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
//=))) địa chỉ hợp đồng thấy giống t rồi mà nhỉ
//mọi thứ ổn r chứ?
//Có vẻ là oke r ô
//public thêm mấy cái nft nữa là đc r
//ô public thì ưu tiên mấy cái thuốc với vòng nháokkk
//Giờ bạn push hộ t cái hàm buy lên main nhé
//Ô mới thêm trong này r hả
//đr, ngay trên đó ok nha
//ổn r tôi out nhé oke oo
        public async void Buyout_Auction()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.BuyoutAuction("0");
                //Debugger.Instance.Log("[Buyout_Auction] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Buyout_Auction] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        public async void Accept_Offer()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.AcceptOffer("0");
                //Debugger.Instance.Log("[Accept_Offer] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Accept_Offer] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        // Cancelling

        public async void Cancel_Listing()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.DirectListings.CancelListing("2");
                //Debugger.Instance.Log("[Cancel_Listing] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Cancel_Listing] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        public async void Cancel_Auction()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.EnglishAuctions.CancelAuction("0");
                //Debugger.Instance.Log("[Cancel_Auction] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Cancel_Auction] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }

        public async void Cancel_Offer()
        {
            try
            {
                //Debugger.Instance.Log("Request Sent", "Pending confirmation...");
                Contract contract = ThirdwebManager.Instance.SDK.GetContract(MARKETPLACE_CONTRACT);
                var result = await contract.Marketplace.Offers.CancelOffer("0");
                //Debugger.Instance.Log("[Cancel_Offer] Success", result.ToString());
            }
            catch (System.Exception e)
            {
                //Debugger.Instance.Log("[Cancel_Offer] Error", e.Message);
                Debug.Log(e.ToString());
            }
        }
        
    }
}

