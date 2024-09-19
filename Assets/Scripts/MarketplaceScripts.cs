using System;
using System.Collections;
using System.Collections.Generic;
using Thirdweb;
using UnityEngine;

public class MarketplaceManager : MonoBehaviour
{
    public static MarketplaceManager Instance { get; private set; }
    public string Address { get; private set; }
    private string contractAddress = "0xCf74b8FC8bE2e6c0D468b06333a1e825a86F9A09";
    private Contract contract;
    private Marketplace marketplace;

    private CreateListingInput input;
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
        contract = ThirdwebManager.Instance.SDK.GetContract(contractAddress);
        if (contract == null)
        {
            Debug.LogError($"Failed to get contract at address: {contractAddress}");
            return;
        }

        marketplace = contract.Marketplace;
        if (marketplace == null)
        {
            Debug.LogError("Failed to get marketplace from contract.");
            return;
        }

        Debug.Log("MarketplaceManager initialized successfully.");
    }

    public async void BuyFromListing(string listingID, string quantity)
    {
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        try
        {
            var result = await marketplace.DirectListings.BuyFromListing(listingID, quantity, Address);
            Debug.Log($"Successfully bought from listing with ID: {listingID}, Quantity: {quantity}, Wallet Address: {Address}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error buying from listing: {ex.Message}");
        }
    }

    public async void GetListing(string id) {
        var listing = await marketplace.DirectListings.GetListing(id);
    }

    public async void IsApproved(string id) {  // Check if the buyer is approved to buy the listing
        Address = await ThirdwebManager.Instance.SDK.Wallet.GetAddress();
        var approved = await marketplace.DirectListings.IsBuyerApprovedForListing(id, Address);
    }

    public async void CreateListing()
    {
        var assetContractAddress = "0x5a7Aa3A0Bb7fdf89ac03D2f782a42649A4dEb2cf";
        var tokenId = "1";
        var pricePerToken = "100000000000000";
        input = new CreateListingInput
        {
            assetContractAddress = assetContractAddress,
            tokenId = tokenId,
            pricePerToken = pricePerToken
        };

        try
        {
            var result = await marketplace.DirectListings.CreateListing(input);
            Debug.Log($"Successfully created listing with Asset Contract Address: {assetContractAddress}, Token ID: {tokenId}, Price Per Token: {pricePerToken}, Wallet Address: {Address}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error creating listing: {ex.Message}");
        }
    }

}
