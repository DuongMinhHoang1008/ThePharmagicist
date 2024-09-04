using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Set a HealthBar for player
    [SerializeField] HealthBar playerHeath;


    //Player attribute
    private float maxHP = 100;
    private float currentHP;
    public float playerVelocity = 5;

    [SerializeField] Element playerElement;


    void Start()
    {
        currentHP = maxHP;
        //If the HealthBar follow character, uncomment line 25
        //playerHeath = GetComponentInChildren<HealthBar>();
    }

    void Update()
    {
        if(currentHP > 0) playerMovement();
        else playerHeath.Die();
    }

    void playerMovement()
    {
        if ( Input.GetKey(KeyCode.W) )
        {
            transform.position = transform.position + new Vector3(0, playerVelocity, 0) * Time.deltaTime;
        }

        if ( Input.GetKey(KeyCode.A) )
        {
            transform.position = transform.position - new Vector3(playerVelocity, 0, 0) * Time.deltaTime;
        }

        if ( Input.GetKey(KeyCode.D) )
        {
            transform.position = transform.position + new Vector3(playerVelocity, 0, 0) * Time.deltaTime;
        }

        if ( Input.GetKey(KeyCode.S) )
        {
            transform.position = transform.position - new Vector3(0, playerVelocity, 0) * Time.deltaTime;
        }
    }

    public void OnMouseDown()
    {
        currentHP -= 10;
        playerHeath.updateHealthBar(currentHP, maxHP);
    }
}
