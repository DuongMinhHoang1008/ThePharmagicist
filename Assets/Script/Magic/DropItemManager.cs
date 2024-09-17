using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public ItemClass itemClass {get; private set;}
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUp(Sprite sprite, ItemClass item) {
        spriteRenderer.sprite = sprite;
        itemClass = item;
    }
}
