using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnoffManager : MonoBehaviour
{
    //private UIManager UIManager;
    /*void Start()
    {
        UIManager = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.Show();
        }
    }*/
    public GameObject uiManagerObject; // Kéo thả GameObject chứa UIManager vào từ Editor

    private UIManager uiManager;

    void Start()
    {
        
            uiManager = uiManagerObject.GetComponent<UIManager>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (uiManagerObject != null && !uiManagerObject.activeInHierarchy)
            {
                uiManagerObject.SetActive(true); // Bật lại GameObject của UIManager
                uiManager.Show();
            }
            else if (uiManager != null)
            {
                uiManager.Show(); // Hiển thị UI nếu nó đã hoạt động
            }
        }
    }
}
