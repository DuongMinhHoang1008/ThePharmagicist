using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using MetaMask.Editor.NaughtyAttributes;
using TMPro;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Quaternion = UnityEngine.Quaternion;
using Unity.Mathematics;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Element element;
    [SerializeField] HealthBar enemyHealthBar;
    [SerializeField] float senseRange = 10f;
    [SerializeField] float speed = 3f;
    [SerializeField] float damage = 10f;
    [SerializeField] float attackCooldown = 1f;
    Rigidbody2D rigidbody2D;
    float health;
    [SerializeField] float maxHealth = 30;
    bool chase = false;
    bool onAttackCooldown = false;
    [SerializeField] GameObject burnIcon;
    [SerializeField] GameObject poisonIcon;
    [SerializeField] GameObject stunIcon;
    [SerializeField] Trigger attackTrigger;
    Vector2 startPosition = Vector2.zero;
    Dictionary<StatusEffect, bool> statusEffects = new Dictionary<StatusEffect, bool>()
                                                    {   
                                                        {StatusEffect.Burn, false},
                                                        {StatusEffect.Poison, false},
                                                        {StatusEffect.Stun, false}
                                                    };
    
    [SerializeField] GameObject elementIcon;
    [SerializeField] GameObject damageNumber;
    public bool comeBack = false;
    bool hasChase = false;
    public bool attack = false;
    Animator animator;
    [SerializeField] bool canDropItem;
    [ShowIf("canDropItem")] [SerializeField] ItemClass veryhighRate;
    [ShowIf("canDropItem")] [SerializeField] ItemClass highRate;
    [ShowIf("canDropItem")] [SerializeField] ItemClass mediumRate;
    [ShowIf("canDropItem")] [SerializeField] ItemClass lowRate;
    [ShowIf("canDropItem")] [SerializeField] ItemClass verylowRate;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemyHealthBar.updateHealthBar(health, maxHealth);
        rigidbody2D = GetComponent<Rigidbody2D>();
        burnIcon.SetActive(false);
        poisonIcon.SetActive(false);
        stunIcon.SetActive(false);
        attackTrigger.OnTriggerStayed2D += OnAttackTriggerStayed2D;
        attackTrigger.OnTriggerExited2D += OnAttackTriggerExit2D;
        elementIcon.GetComponent<SpriteRenderer>().sprite = GlobalGameVar.Instance().elementDic[element].sprite;
        animator = GetComponent<Animator>();
        startPosition = rigidbody2D.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = PlayerInfo.Instance().GetPlayerPos() + Vector2.up - rigidbody2D.position;
        if (Vector2.Distance(PlayerInfo.Instance().GetPlayerPos(), rigidbody2D.position) < senseRange) {
            chase = true;
            comeBack = false;
            if (!hasChase) hasChase = true;
        } else {
            chase = false;
            direction = startPosition - rigidbody2D.position;
            if (hasChase) {
                comeBack = true;
                hasChase = false;
            }
            
        }
        animator.SetFloat("FloatX", direction.x);
        animator.SetFloat("FloatY", direction.y);
        animator.SetFloat("Speed", direction.magnitude * ((chase ^ comeBack)? 1 : 0));
    }
    private void FixedUpdate() {
        if(!attack) {
            Vector2 destination = startPosition;
            if (chase && !statusEffects[StatusEffect.Stun]) {
                destination = PlayerInfo.Instance().GetPlayerPos() + Vector2.up;
            }
            Vector2 direction = (destination - rigidbody2D.position).normalized;
            if (!chase && Vector2.Distance(destination, rigidbody2D.position) < 1f) { comeBack = false; return;}
            rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.deltaTime);
        }
    }
    public void TakeDamage(float damage, StatusEffect status, int level, Element dmgElement) {
        if (health > 0) {
            StartCoroutine("Blinking");
            float damageModifier = 1f;
            Color dmgColor = Color.white;
            Dictionary<Element, ElementInfo> elementDic = GlobalGameVar.Instance().elementDic;
            if (elementDic[dmgElement].minus == element) {
                damageModifier = 1.5f;
                dmgColor = Color.red;
            } else if (elementDic[dmgElement].plus == element) {
                damageModifier = 0.5f;
                dmgColor = Color.gray;
            }
            ShowDamageRecieve(damage * damageModifier, dmgColor);
            health -= damage * damageModifier;
            enemyHealthBar.updateHealthBar(health, maxHealth);
            if (status != StatusEffect.None && !statusEffects[status]) {
                statusEffects[status] = true;
                OnStatusEffect(status, level);
            }
            if (health <= 0) {
                DropItem();
                Destroy(gameObject);
            }
        }
    }
    void LoseStatusHealth(float damage, int level, Color dmgColor) {
        if (health > 0) {
            int dmg = (int) Math.Ceiling(damage * Math.Pow(1.25f ,level - 1));
            health -= dmg;
            ShowDamageRecieve(dmg, dmgColor);
            enemyHealthBar.updateHealthBar(health, maxHealth);
            if (health <= 0) {
                DropItem();
                Destroy(gameObject);
            }
        }
    }
    void OnStatusEffect(StatusEffect status, int level) {
        switch (status) {
            case StatusEffect.Burn:
                StartCoroutine("OnBurn", level);
                break;
            case StatusEffect.Poison:
                StartCoroutine("OnPoison", level);
                break;
            case StatusEffect.Stun:
                StartCoroutine("OnStun", level);
                break;
            default:
                break;
        }
    }
    IEnumerator OnBurn(int level) {
        burnIcon.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        LoseStatusHealth(8, level, Color.red);
        statusEffects[StatusEffect.Burn] = false;
        burnIcon.SetActive(false);
    }
    IEnumerator OnPoison(int level) {
        poisonIcon.SetActive(true);
        for (int i = 0; i < 8; i++) {
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            LoseStatusHealth(1.5f, level, Color.green);
        }
        statusEffects[StatusEffect.Poison] = false;
        poisonIcon.SetActive(false);
    }
    IEnumerator OnStun(int level) {
        stunIcon.SetActive(true);
        yield return new WaitForSeconds((int) Math.Ceiling(Math.Pow(1.25f ,level - 1)));
        statusEffects[StatusEffect.Stun] = false;
        stunIcon.SetActive(false);
    }
    IEnumerator Blinking() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    void OnAttackTriggerStayed2D(Collider2D collider) {
        if (collider.GetComponent<Player>() != null) {
            Player player = collider.GetComponent<Player>();
            if (!onAttackCooldown) {
                attack = true;
                float damageModifier = 1;
                Element playerElement = PlayerInfo.Instance().element;
                Dictionary<Element, ElementInfo> elementDic = GlobalGameVar.Instance().elementDic;
                if (elementDic[element].minus == playerElement) {
                    damageModifier = 1.5f;
                } else if (elementDic[element].plus == playerElement) {
                    damageModifier = 0.5f;
                }
                StartCoroutine(AttackPlayer(player, damage * damageModifier));
            }
        }
    }
    void OnAttackTriggerExit2D(Collider2D collider) {
        if (collider.GetComponent<Player>() != null) {
            attack = false;
        }
    }
    IEnumerator AttackPlayer(Player player, float dmg) {
        onAttackCooldown = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        if (attack) {
            player.ChangeHealth(-dmg);
        }
        Invoke("AttackCooldownEnd", attackCooldown - 0.5f);
    }
    void AttackCooldownEnd() {
        onAttackCooldown = false;
    }
    void DropItem() {
        if (canDropItem) {
            ItemClass itemDrop;
            int rate = UnityEngine.Random.Range(0, 100);
            if (rate >= 99) {
                //2%
                itemDrop = verylowRate;
            } else if (rate >= 97) {
                //2%
                itemDrop = lowRate;
            } else if (rate >= 92) {
                //5%
                itemDrop = mediumRate;
            } else if (rate >= 82) {
                //10%
                itemDrop = highRate;
            } else if (rate >= 62) {
                //20%
                itemDrop = veryhighRate;
            } else {
                itemDrop = null;
            }
            if (itemDrop != null) {
                GameObject itemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Enemies/DropItem.prefab");
                GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                item.GetComponent<DropItemManager>().SetUp(itemDrop.itemIcon, itemDrop);
            }
        }
    }
    void ShowDamageRecieve(float dmg, Color dmgColor) {
        if (damageNumber != null) {
            int dam = (int) Math.Ceiling(dmg);
            GameObject showdmg = Instantiate(damageNumber, rigidbody2D.position + Vector2.up, Quaternion.identity);
            showdmg.GetComponent<TextMeshPro>().text = dam.ToString();
            showdmg.GetComponent<TextMeshPro>().color = dmgColor;
            float randRad = UnityEngine.Random.Range(60, 120) * Mathf.Deg2Rad;
            Vector2 direc = new Vector2(Mathf.Cos(randRad), Mathf.Sin(randRad));
            showdmg.GetComponent<Rigidbody2D>().AddForce(direc * UnityEngine.Random.Range(300, 400));
        }
    }
}
