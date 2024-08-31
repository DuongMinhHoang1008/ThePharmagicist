using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only Block is child
public class BlockGroup : MonoBehaviour
{
    [SerializeField] public Block[] blockArr;
    [SerializeField] Element element;
    Camera mainCamera;
    // Start is called before the first frame update
    private void Awake() {
        
    }
    void Start()
    {
        mainCamera = Camera.main;
        blockArr = new Block[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            blockArr[i] = transform.GetChild(i).GetComponent<Block>();
            blockArr[i].SetElement(element);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(blockArr.Length);
    }
    private void OnMouseDrag() {
        Vector3 screenPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = screenPos;
    }
    private void OnMouseUp() {
        if (IsPlacable()) {
            foreach (Block block in blockArr) {
                block.Place();
            }  
            Destroy(gameObject);
        }
    }
    bool IsPlacable() {
        foreach (Block block in blockArr) {
            if (!block.IsPlacable()) {
                Debug.Log(block.name);
                return false;
            }
        }
        return true;
    }
}
