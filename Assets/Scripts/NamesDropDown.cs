using UnityEngine;
using TMPro;

public class NamesDropDown : MonoBehaviour {
    private ConfigManager configManager;
    private TMP_Dropdown dropdown;
    public RectTransform template;         // Template do dropdown
    public RectTransform content;          // Content dentro do ScrollRect
    public float optionHeight = 20f;       // Altura de cada opção
    public int maxVisibleOptions = 5;      // Máximo de opções visíveis

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        configManager = GetComponentInParent<ConfigManager>();
        Transform arrow = dropdown.transform.Find("Arrow");

        if (configManager.config.players.Length == 0)
        {
            dropdown.options.Clear();
            dropdown.options.Add(new TMP_Dropdown.OptionData("Nenhum Jogador Encontrado"));
            dropdown.interactable = false;
            arrow.gameObject.SetActive(false);
            return;
        }
        else
        {
            LoadPlayers();
        }

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        GameData.Instance.selectedPlayer = configManager.config.players[dropdown.value];
        //AdjustTemplateHeight();

    }

    public void LoadPlayers()
    {
        dropdown.options.Clear();
            for (int i = 0; i < configManager.config.players.Length; i++)
            {
                TMP_Dropdown.OptionData option = new(configManager.config.players[i].name);
                dropdown.options.Add(option);
            }
            dropdown.value = 0;
            dropdown.RefreshShownValue();
    }

    void AdjustTemplateHeight() {
        int optionCount = dropdown.options.Count;

        int visibleCount = Mathf.Min(optionCount, maxVisibleOptions);

        float newHeight = visibleCount * optionHeight;

        template.sizeDelta = new Vector2(template.sizeDelta.x, newHeight);
        content.sizeDelta = new Vector2(content.sizeDelta.x, optionCount * optionHeight);
        
    }

    void OnDropdownValueChanged(int value) {
        GameData.Instance.selectedPlayer = configManager.config.players[value];
    }
}
