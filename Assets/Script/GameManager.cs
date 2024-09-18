using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}
    string lastScene;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSceneToBrewing() {
        SceneManager.LoadScene("BrewingPuzzle");
    }
    public void ChangeSceneToInventory() {
        SceneManager.LoadScene("InventoryTestScene");
    }
    public string GetLastScene() {
        return lastScene;
    }
    public void ChangeLastScene(string scene) {
        lastScene = scene;
    }
}
