using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigFormEdit : MonoBehaviour
{

    [SerializeField] private TMP_InputField playerName;
    [SerializeField] private Toggle toggleFancyGraphics;
    [SerializeField] private Toggle toggleRandomPieceOrder;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleMultipleHints;
    [SerializeField] private Toggle toggleShowBoy;
    [SerializeField] private Toggle toggleShowGirl;
    [SerializeField] private TMP_InputField refHintTime;
    [SerializeField] private TMP_InputField hintTime;

    private ChangeScenes changeScenes;

    private ConfigManager configManager;

    void Start()
    {
        configManager = GetComponentInParent<ConfigManager>();
        changeScenes = GetComponent<ChangeScenes>();

        if (GameData.Instance.selectedPlayer != null)
        {
            LoadData();
        }

    }

    public void LoadData()
    {
        if (configManager == null)
        {
            Debug.LogError("ConfigManager not found in the scene.");
            return;
        }

        Config config = configManager.config;
        if (config == null)
        {
            Debug.LogError("Config is null.");
            return;
        }

        Player player = GameData.Instance.selectedPlayer;

        if (player != null)
        {
            playerName.text = player.name;
            toggleFancyGraphics.isOn = player.fancyGraphicsOn;
            toggleRandomPieceOrder.isOn = player.randomPieceOrderOn;
            toggleMusic.isOn = player.isMusicOn;
            toggleMultipleHints.isOn = player.isMultipleHintsOn;
            toggleShowBoy.isOn = player.showBoyOn;
            toggleShowGirl.isOn = player.showGirlOn;
            refHintTime.text = player.refHintTime.ToString();
            hintTime.text = player.hintTime.ToString();
        }
    }

    public void EditData()
    {
        if (configManager == null)
        {
            Debug.LogError("ConfigManager not found in the scene.");
            return;
        }

        Config config = configManager.config;
        if (config == null)
        {
            Debug.LogError("Config is null.");
            return;
        }

        Player newData = new()
        {
            name = playerName.text,
            fancyGraphicsOn = toggleFancyGraphics.isOn,
            randomPieceOrderOn = toggleRandomPieceOrder.isOn,
            isMusicOn = toggleMusic.isOn,
            isMultipleHintsOn = toggleMultipleHints.isOn,
            showBoyOn = toggleShowBoy.isOn,
            showGirlOn = toggleShowGirl.isOn
        };

        if (int.TryParse(refHintTime.text, out int refHintTimeValue))
        {
            newData.refHintTime = refHintTimeValue;
        }
        else
        {
            Debug.LogError("Invalid input for refHintTime.");
            return;
        }

        if (int.TryParse(hintTime.text, out int hintTimeValue))
        {
            newData.hintTime = hintTimeValue;
        }
        else
        {
            Debug.LogError("Invalid input for hintTime.");
            return;
        }

        configManager.EditPlayerConfig(GameData.Instance.selectedPlayer, newData);
        changeScenes.ReturnToPreviousScene();
        
    }
    
}
