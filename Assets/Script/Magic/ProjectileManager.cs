using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Element element;
    float damage;
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
    public void Launch(Vector2 direction, float force, Element el, float dmg, float ltime) {
        element = el;
        damage = dmg;
        if (rigidbody2D != null) {
            Debug.Log(direction);
            rigidbody2D.AddForce(direction * force);
        }
        Invoke("SelfDestroy", ltime);
    }
    void SelfDestroy() {
        Destroy(gameObject);
    }
}
