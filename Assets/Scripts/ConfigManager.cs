using UnityEngine;
using System.IO;

[System.Serializable] public class Config {
    public Player[] players;
}

[System.Serializable] public class Player {
    public string name;
    public bool fancyGraphicsOn;
    public bool randomPieceOrderOn;
    public bool isMusicOn;
    public bool isMultipleHintsOn;
    public bool showBoyOn;
    public bool showGirlOn;
    public int refHintTime;
    public int hintTime;
    //public int[] sceneOrder;

}


public class ConfigManager : MonoBehaviour
{

    private string configPath;
    public Config config;

    void Awake()
    {
        configPath = Path.Combine(Application.persistentDataPath, "config.json");
        Debug.Log("Config path: " + configPath);

        if (File.Exists(configPath))
        {
            string json = File.ReadAllText(configPath);
            config = JsonUtility.FromJson<Config>(json);
            Debug.Log("Config loaded: " + json);

        }
        else
        {
            config = new Config();
            SaveConfigFile();
            Debug.Log("Config file not found, created new one.");
        }

    }

    void Update()
    {

    }

    private void SaveConfigFile()
    {
        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(configPath, json);
        Debug.Log("Config saved: " + json);
    }

    public void SavePlayerConfig(Player newPlayer)
    {
        config.players ??= new Player[0];

        Player[] newPlayers = new Player[config.players.Length + 1];

        for (int i = 0; i < config.players.Length; i++)
        {
            newPlayers[i] = config.players[i];
        }

        newPlayers[newPlayers.Length - 1] = newPlayer;
        config.players = newPlayers;

        SaveConfigFile();
    }

    public void EditPlayerConfig(Player oldData, Player newData)
    {
        for (int i = 0; i < config.players.Length; i++)
        {
            if (config.players[i].name == oldData.name)
            {
                config.players[i] = newData;
                break;
            }
        }

        SaveConfigFile();
    } 

}
