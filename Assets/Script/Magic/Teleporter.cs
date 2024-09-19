using System;
using System.Collections;
using System.Collections.Generic;
using MetaMask.Editor.NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum TeleportMode {
    InScene = 0,
    BetweenScenes = 1
}

public class Teleporter : MonoBehaviour
{
    [SerializeField] TeleportMode mode;
    [ShowIf("mode", TeleportMode.BetweenScenes)] [SerializeField] string sceneTeleTo;
    [ShowIf("mode", TeleportMode.InScene)] [SerializeField] Vector2 positionTeleTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<Player>() != null) {
            if (mode == TeleportMode.BetweenScenes) {
                SceneManager.LoadScene(sceneTeleTo);
            } else {
                other.GetComponent<Rigidbody2D>().position = positionTeleTo;
            }
            
        }
    }
    public void ChangeScene() {
        if (mode == TeleportMode.BetweenScenes) {
            SceneManager.LoadScene(sceneTeleTo);
        }
    }
}
