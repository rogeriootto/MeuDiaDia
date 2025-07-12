using TMPro;
using UnityEngine;

public class ConfigDelName : MonoBehaviour
{
    private TMP_Text playerNameText;

    void Start()
    {
        playerNameText = GetComponent<TMP_Text>();
        if (GameData.Instance.selectedPlayer != null)
        {
            playerNameText.text = "Tem certeza que quer deletar " + GameData.Instance.selectedPlayer.name + "?";
        }
        
    }

}
