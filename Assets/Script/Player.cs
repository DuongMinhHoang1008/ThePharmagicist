using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class Player : MonoBehaviour
{
    //Player attribute
    [SerializeField] HealthBar playerHealth;
    private float maxHP = 100;
    private float currentHP;
    public float playerVelocity = 5;
    private Vector3 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] Element playerElement;
    
    [SerializeField]private SpawnitemManager[] spawnitemManagers;
    Vector2 direction ;
    
    public LayerMask lootLayer;

    void Start()
    {
        
        //spawnitemManagers = FindObjectsOfType<SpawnitemManager>();
        lootLayer = ~LayerMask.GetMask("Player");
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //If the HealthBar follow character, uncomment line 25
        //playerHeath = GetComponentInChildren<HealthBar>();
        playerHealth.updateHealthBar(currentHP, maxHP);
    }

    void Update()
    {
        if (currentHP > 0) playerMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            RaycastHit2D[] hits = Physics2D.RaycastAll(this.transform.position, transform.right , 1f, LayerMask.GetMask("Item", "~Player"));
            //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), hit.collider);
            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null && hit.collider.gameObject.layer == 6)
                    {
                        SpawnitemManager spawnitemManager = hit.collider.GetComponent<SpawnitemManager>();
                        if (spawnitemManager != null)
                        {
                            hit.collider.GetComponent<SpawnitemManager>().Loot();
                            break;
                        }
                    }
                }
            }
        }
        
    }

    void FixedUpdate()
    {
        if(currentHP > 0) rb.MovePosition(rb.position + (Vector2) moveInput * playerVelocity * Time.fixedDeltaTime);
    }

    void playerMovement()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        if ((Vector2) moveInput != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", moveInput.x);
            animator.SetFloat("LastVertical", moveInput.y);
        }
    }

    
}

