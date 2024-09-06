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
    private Vector3 moveInput;

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
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        transform.position += moveInput * playerVelocity * Time.deltaTime;
    }

    public void OnMouseDown()
    {
        currentHP -= 10;
        playerHeath.updateHealthBar(currentHP, maxHP);
    }
}
