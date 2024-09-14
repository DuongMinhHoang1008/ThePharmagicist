using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] HealthBar enemyHealthBar;
    [SerializeField] float senseRange = 10f;
    [SerializeField] float speed = 3f;
    Rigidbody2D rigidbody2D;
    float health;
    [SerializeField] float maxHealth = 30;
    bool chase = false;
    [SerializeField] GameObject burnIcon;
    [SerializeField] GameObject poisonIcon;
    [SerializeField] GameObject stunIcon;
    Dictionary<StatusEffect, bool> statusEffects = new Dictionary<StatusEffect, bool>()
                                                    {   
                                                        {StatusEffect.Burn, false},
                                                        {StatusEffect.Poison, false},
                                                        {StatusEffect.Stun, false}
                                                    };
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        enemyHealthBar.updateHealthBar(health, maxHealth);
        rigidbody2D = GetComponent<Rigidbody2D>();
        burnIcon.SetActive(false);
        poisonIcon.SetActive(false);
        stunIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(PlayerInfo.Instance().GetPlayerPos(), rigidbody2D.position) < senseRange) {
            chase = true;
        } else {
            chase = false;
        }
    }
    private void FixedUpdate() {
        if (chase && !statusEffects[StatusEffect.Stun]) {
            Vector2 destination = PlayerInfo.Instance().GetPlayerPos();
            Vector2 direction = (destination - rigidbody2D.position).normalized;
            rigidbody2D.MovePosition(rigidbody2D.position + direction * speed * Time.deltaTime);
        }
    }
    public void TakeDamage(float damage, StatusEffect status) {
        StartCoroutine("Blinking");
        health -= damage;
        enemyHealthBar.updateHealthBar(health, maxHealth);
        if (status != StatusEffect.None && !statusEffects[status]) {
            statusEffects[status] = true;
            OnStatusEffect(status);
        }
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    void LoseStatusHealth(float damage) {
        health -= damage;
        enemyHealthBar.updateHealthBar(health, maxHealth);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    void OnStatusEffect(StatusEffect status) {
        switch (status) {
            case StatusEffect.Burn:
                StartCoroutine("OnBurn");
                break;
            case StatusEffect.Poison:
                StartCoroutine("OnPoison");
                break;
            case StatusEffect.Stun:
                StartCoroutine("OnStun");
                break;
            default:
                break;
        }
    }
    IEnumerator OnBurn() {
        burnIcon.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        LoseStatusHealth(8);
        statusEffects[StatusEffect.Burn] = false;
        burnIcon.SetActive(false);
    }
    IEnumerator OnPoison() {
        poisonIcon.SetActive(true);
        for (int i = 0; i < 8; i++) {
            yield return new WaitForSeconds(0.5f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            LoseStatusHealth(1.5f);
        }
        statusEffects[StatusEffect.Poison] = false;
        poisonIcon.SetActive(false);
    }
    IEnumerator OnStun() {
        stunIcon.SetActive(true);
        yield return new WaitForSeconds(1f);
        statusEffects[StatusEffect.Stun] = false;
        stunIcon.SetActive(false);
    }
    IEnumerator Blinking() {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
