using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Element element;
    float damage;
    bool isExplode = false;
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
    public void Launch(Vector2 direction, float force, Element el, float dmg, float ltime, bool explode, StatusEffect status) {
        element = el;
        damage = dmg;
        isExplode = explode;
        statusEffect = status;
        if (rigidbody2D != null) {
            Debug.Log(direction);
            rigidbody2D.AddForce(direction * force);
        }
        Invoke("SelfDestroy", ltime);
    }
    void SelfDestroy() {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<EnemyController>() != null) {
            other.GetComponent<EnemyController>().TakeDamage(damage, statusEffect);
            if (isExplode) {
                Invoke("SelfDestroy", 0.5f);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
