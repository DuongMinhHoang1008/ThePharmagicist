using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Set a HealthBar for player
    [SerializeField] HealthBar playerHeath;

    //Player attribute
    private const float maxHP = 100;
    private float currentHP;
    public float playerVelocity = 5;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] Element playerElement;


    void Start()
    {
        currentHP = maxHP;
        playerHeath = GetComponent<HealthBar>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(currentHP > 0) playerMovement();
        else playerHeath.Die();
    }

    void playerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        rb.MovePosition(rb.position + moveInput * playerVelocity * Time.fixedDeltaTime);
        
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", moveInput.x);
            animator.SetFloat("LastVertical", moveInput.y);
        }
    }

}
