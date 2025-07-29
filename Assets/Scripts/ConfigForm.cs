using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigForm : MonoBehaviour
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
    }

    public void SaveData()
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

        Player player = new()
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
            player.refHintTime = refHintTimeValue;
        }
        else
        {
            Debug.LogError("Invalid input for refHintTime.");
            return;
        }

        if (int.TryParse(hintTime.text, out int hintTimeValue))
        {
            player.hintTime = hintTimeValue;
        }
        else
        {
            Debug.LogError("Invalid input for hintTime.");
            return;
        }

        configManager.SavePlayerConfig(player);
        changeScenes.ReturnToPreviousScene();
    }
    
}
