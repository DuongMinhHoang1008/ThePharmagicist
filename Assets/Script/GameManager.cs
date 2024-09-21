using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}
    string lastScene;
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
    public void ChangeElement(string element) {
        //PlayerInfo.Instance().SetPlayerElement(element);
        Element el = Element.None;
        switch(element){
            case "Metal":
                el = Element.Metal;
                break;
            case "Water":
                el = Element.Water;
                break;
            case "Wood":
                el = Element.Wood;
                break;
            case "Fire":
                el = Element.Fire;
                break;
            case "Earth":
                el = Element.Earth;
                break;
            default:
                break;
        }
        PlayerInfo.Instance().SetPlayerElement(el);
    }
}
