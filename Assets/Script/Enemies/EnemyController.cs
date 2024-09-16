using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    public bool comeBack = false;
    bool hasChase = false;
    Animator animator;
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
        elementIcon.GetComponent<SpriteRenderer>().sprite = GlobalGameVar.Instance().elementDic[element].sprite;
        animator = GetComponent<Animator>();
        startPosition = rigidbody2D.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = PlayerInfo.Instance().GetPlayerPos() - rigidbody2D.position;
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
        Vector2 destination = startPosition;
        if (chase && !statusEffects[StatusEffect.Stun]) {
            destination = PlayerInfo.Instance().GetPlayerPos();
        }
        Vector2 direction = (destination - rigidbody2D.position).normalized;
        if (!chase && Vector2.Distance(destination, rigidbody2D.position) < 1f) { comeBack = false; return;}
        rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.deltaTime);
    }
    public void TakeDamage(float damage, StatusEffect status, int level, Element dmgElement) {
        StartCoroutine("Blinking");
        float damageModifier = 1;
        Dictionary<Element, ElementInfo> elementDic = GlobalGameVar.Instance().elementDic;
        if (elementDic[dmgElement].minus == element) {
            damageModifier = 1.5f;
        } else if (elementDic[dmgElement].minus == element) {
            damageModifier = 0.5f;
        }
        health -= damage * damageModifier;
        enemyHealthBar.updateHealthBar(health, maxHealth);
        if (status != StatusEffect.None && !statusEffects[status]) {
            statusEffects[status] = true;
            OnStatusEffect(status, level);
        }
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    void LoseStatusHealth(float damage, int level) {
        health -= (int) Math.Ceiling(damage * Math.Pow(1.25f ,level - 1));
        enemyHealthBar.updateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(gameObject);
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
        LoseStatusHealth(8, level);
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
            LoseStatusHealth(1.5f, level);
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
                float damageModifier = 1;
                Element playerElement = PlayerInfo.Instance().element;
                Dictionary<Element, ElementInfo> elementDic = GlobalGameVar.Instance().elementDic;
                if (elementDic[element].minus == playerElement) {
                    damageModifier = 1.5f;
                } else if (elementDic[element].plus == playerElement) {
                    damageModifier = 0.5f;
                }
                player.ChangeHealth(-damage * damageModifier);
                onAttackCooldown = true;
                Invoke("AttackCooldownEnd", attackCooldown);
            }
        }
    }
    void AttackCooldownEnd() {
        onAttackCooldown = false;
    }
}
