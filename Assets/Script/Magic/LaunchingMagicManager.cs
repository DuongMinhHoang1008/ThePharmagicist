using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Magic {
    [SerializeField]
    public ScriptableMagic scriptableMagic;
    [SerializeField]
    public int level = 1;
}

public class LaunchingMagicManager : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Magic firstMagic;
    [SerializeField] Magic secondMagic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    public void Launch(Vector2 position, Vector2 direction, Magic magic) {
        Debug.Log(Vector2.SignedAngle(direction, (direction.x >= 0) ? Vector2.right : Vector2.left));
        GameObject launch = Instantiate(projectile, position + Vector2.up * 0.75f, Quaternion.Euler(0,0, Vector2.SignedAngle((direction.x >= 0) ? Vector2.right : Vector2.left, direction)));
        Debug.Log(magic.scriptableMagic);
        if (direction.x < 0) {
            launch.GetComponent<SpriteRenderer>().flipX = true;
        }
        launch.GetComponent<ProjectileManager>().Launch(direction, 300f, magic.scriptableMagic.element, magic.scriptableMagic.damage, magic.scriptableMagic.lifeTime);
    }
    public void LaunchFirstMagic(Vector2 position, Vector2 direction) {
        Launch(position, direction, firstMagic);
    }
}
