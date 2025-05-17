using UnityEngine;

public class GameData : MonoBehaviour {
    public static GameData Instance;
    public Player selectedPlayer;
    public string currentScene;
    public string previousScene;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else {
            Destroy(gameObject); 
        }
    }
}