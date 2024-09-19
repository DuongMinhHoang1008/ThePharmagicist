using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Asn1.Icao;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Element element;
    float damage;
    bool isExplode = false;
    int level = 1;
    StatusEffect statusEffect;
    // Start is called before the first frame update
    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch(Vector2 direction, float force, Element el, float dmg, float ltime, bool explode, StatusEffect status, int lv) {
        element = el;
        float damageModifier = 1;
        Element playerElement = PlayerInfo.Instance().element;
        Dictionary<Element, ElementInfo> elementDic = GlobalGameVar.Instance().elementDic;
        if (element == playerElement) {
            damageModifier = 1.5f;
        } else if (elementDic[element].minus == playerElement || elementDic[playerElement].minus == element) {
            damageModifier = 0.5f;
        }
        damage = dmg * damageModifier;
        isExplode = explode;
        statusEffect = status;
        level = lv;
        if (rigidbody2D != null) {
            rigidbody2D.AddForce(direction * force);
        }
        Invoke("SelfDestroy", ltime);
    }
    void SelfDestroy() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<EnemyController>() != null) {
            other.GetComponent<EnemyController>().TakeDamage(damage, statusEffect, level, element);
            if (isExplode) {
                Invoke("SelfDestroy", 0.5f);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
