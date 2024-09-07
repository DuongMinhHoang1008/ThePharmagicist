using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class Player : MonoBehaviour
{
    //Player attribute
    private float maxHP = 100;
    private float currentHP;
    public float playerVelocity = 5;
    private Vector3 moveInput;
    
    [SerializeField]private SpawnitemManager[] spawnitemManagers;
    Vector2 direction ;
    
    public LayerMask lootLayer;

    void Start()
    {
        
        //spawnitemManagers = FindObjectsOfType<SpawnitemManager>();
        lootLayer = ~LayerMask.GetMask("Player");
        currentHP = maxHP;
        //If the HealthBar follow character, uncomment line 25
        //playerHeath = GetComponentInChildren<HealthBar>();
    }

    void Update()
    {
        if (currentHP > 0) playerMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.DrawRay(transform.position, transform.right * 10, Color.red, 2f); // Visualize the ray
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
                        }
                    }
                }
            }
        }
        
    }

    void playerMovement()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        transform.position += moveInput * playerVelocity * Time.deltaTime;
    }

    
}

