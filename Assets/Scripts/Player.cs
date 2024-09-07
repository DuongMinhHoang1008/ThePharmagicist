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
    public float playerVelocity = 10;
    public Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] Element playerElement;


    void Start()
    {
        currentHP = maxHP;
        //If the HealthBar follow character, uncomment line 25
        //playerHeath = GetComponentInChildren<HealthBar>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(currentHP > 0) playerMovement();
        else playerHeath.Die();
    }

    void playerMovement()
    {
        moveInput.x = 0;
        moveInput.y = 1;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + moveInput * playerVelocity * Time.deltaTime);
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", moveInput.x);
            animator.SetFloat("LastVertical", moveInput.y);
        }
    }

    public void OnMouseDown()
    {
        currentHP -= 10;
        playerHeath.updateHealthBar(currentHP, maxHP);
    }
}
