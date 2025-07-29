using UnityEngine;

public class ChangeScenes : MonoBehaviour {

    public void ChangeScene(string sceneName) {
        GameData.Instance.previousScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        GameData.Instance.currentScene = sceneName;
        Debug.Log("previous scene: " + GameData.Instance.previousScene);
        Debug.Log("next scene: " + GameData.Instance.currentScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    
    public void ReturnToPreviousScene() {
        string previousScene = GameData.Instance.previousScene;
        GameData.Instance.currentScene = previousScene;
        Debug.Log("previous scene: " + GameData.Instance.previousScene);
        Debug.Log("next scene: " + GameData.Instance.currentScene);
        UnityEngine.SceneManagement.SceneManager.LoadScene(previousScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void RestartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        GameData.Instance.level = 0;
        if (GameData.Instance.selectedPlayer.showBoyOn && GameData.Instance.selectedPlayer.showGirlOn)
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level-1");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level-1-Menina");
            }
        }
        else
        {
            if (GameData.Instance.selectedPlayer.showBoyOn)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level-1");
            }

            if (GameData.Instance.selectedPlayer.showGirlOn)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level-1-Menina");
            }
        }
    }
}
