using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestroy", 0.5f);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SelfDestroy() {
        Destroy(gameObject);
    }
}
