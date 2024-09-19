using System.Collections;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Engines;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackable : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            LaunchProjectile();
        }
    }
    void LaunchProjectile() {
        
    }
}
