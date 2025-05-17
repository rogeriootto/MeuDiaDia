using TMPro;
using UnityEngine;

public class ConfigEditName : MonoBehaviour
{
    private TMP_Text playerNameText;

    void Start()
    {
        playerNameText = GetComponent<TMP_Text>();
        if (GameData.Instance.selectedPlayer != null)
        {
            playerNameText.text = "Editando " + GameData.Instance.selectedPlayer.name;
        }
        
    }

}
