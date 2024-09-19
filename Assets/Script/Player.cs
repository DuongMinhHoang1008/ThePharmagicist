using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Player attribute
    [SerializeField] HealthBar playerHealth;
    [SerializeField] float invincibleTime = 1f; 
    private float maxHP = 100;
    private float currentHP;
    public float playerVelocity = 5;
    private Vector3 moveInput;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] Element playerElement;
    
    [SerializeField] private SpawnitemManager[] spawnitemManagers;
    Vector2 direction = Vector2.right;
    LaunchingMagicManager launchingMagicManager;
    public LayerMask lootLayer;
    bool isInvincible = false;
    [SerializeField] MagicInventoryClass inventory;
    [SerializeField] AudioSource hitsound;

    void Start()
    {
        
        //spawnitemManagers = FindObjectsOfType<SpawnitemManager>();
        lootLayer = ~LayerMask.GetMask("Player");
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //If the HealthBar follow character, uncomment line 25
        //playerHeath = GetComponentInChildren<HealthBar>();
        launchingMagicManager = GetComponent<LaunchingMagicManager>();
        playerHealth.updateHealthBar(currentHP, maxHP);
        if (PlayerInfo.Instance().element == Element.None) {
            PlayerInfo.Instance().SetPlayerElement(playerElement);
        } else {
            playerElement = PlayerInfo.Instance().element;
        }
        switch (GameManager.instance.GetLastScene()) {
            case "Forest2":
                rb.position = new Vector2(42, 0);
                break;
            case "Forest1":
                if (SceneManager.GetActiveScene().name == "Lobby") {
                    rb.position = new Vector2(19, -17);
                }
                break;
            case "LobbyHouse":
                if (SceneManager.GetActiveScene().name == "Lobby") {
                    rb.position = new Vector2(0, 14);
                }
                break;
            default:
                break;
        }
        GameManager.instance.ChangeLastScene(SceneManager.GetActiveScene().name);
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
                        if (hit.collider.GetComponent<DropItemManager>() != null) {
                            inventory.AddItem(hit.collider.GetComponent<DropItemManager>().itemClass, 1);
                            Destroy(hit.collider.gameObject);
                            break;
                        }
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
        if(launchingMagicManager != null) {
            if(Input.GetMouseButtonDown(0)) {
                Vector2 shootDir = ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position - Vector2.up * 0.75f).normalized;
                launchingMagicManager.LaunchFirstMagic(shootDir);
            }
            if(Input.GetMouseButtonDown(1)) {
                Vector2 shootDir = ((Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position - Vector2.up * 0.75f).normalized;
                launchingMagicManager.LaunchSecondMagic(shootDir);
            }
        } 
    }

    void FixedUpdate()
    {
        if(currentHP > 0) rb.MovePosition(rb.position + (Vector2) moveInput * playerVelocity * Time.fixedDeltaTime);
        PlayerInfo.Instance().UpdatePlayerPos(rb.position);
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
            direction = moveInput;
        }
    }
    public void ChangeHealth(float moreHealth) {
        if (moreHealth < 0) {
            if (isInvincible) {
                return;
            }
            hitsound.PlayOneShot(hitsound.clip, 1);
            isInvincible = true;
            StartCoroutine("Blinking");
            Invoke("InvincibleEnd", invincibleTime);
        }
        currentHP += moreHealth;
        if (currentHP > maxHP) currentHP = maxHP;
        else if (currentHP < 0) currentHP = 0;
        playerHealth.updateHealthBar(currentHP, maxHP);
    }
    void InvincibleEnd() {
        isInvincible = false;
    }
    IEnumerator Blinking() {
        int time = (int)(invincibleTime / 0.2f);
        for (int i = 0; i < time; i++) {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public InventoryManager GetInventory() {
        return inventory;
    }
}

