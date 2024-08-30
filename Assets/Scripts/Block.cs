using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    Camera mainCamera;
    Element element;
    BoardTile boardTile;
    bool placable = false;
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDrag() {
        Vector3 screenPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        transform.position = screenPos;
    }
    private void OnMouseUp() {
        Debug.Log("up" + " " + placable);
        if (placable && boardTile != null) {
            boardTile.ChangeColor(GetComponent<SpriteRenderer>().color);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<BoardTile>() != null) {
            if (Math.Abs(transform.position.x - other.gameObject.transform.position.x) < GlobalGameVar.Instance().blockWidth / 1.5f
            && Math.Abs(transform.position.y - other.gameObject.transform.position.y) < GlobalGameVar.Instance().blockWidth / 1.5f) {
                Debug.Log("Oj");
                placable = true;
                boardTile = other.GetComponent<BoardTile>();
                boardTile.ChangeColor(Color.gray);
            } else {
                placable = false;
                other.GetComponent<BoardTile>().ChangeColor(Color.white);
            }
        }
    }
}
