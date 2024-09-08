using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFManager : MonoBehaviour
{
    void Start()
    {
        Hide();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
