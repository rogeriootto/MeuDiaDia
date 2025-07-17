using UnityEngine;
using UnityEngine.UI;
using System.IO;
using NativeFilePickerNamespace;

public class SaveCSV : MonoBehaviour
{
    private string csvFilePath;
    void Start()
    {
        csvFilePath = Path.Combine(Application.persistentDataPath, "results.csv");
    }

    public void ExportCSVToUser()
    {
        // Verifica se o arquivo CSV existe
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found!");
            return;
        }

        NativeFilePicker.ExportFile(csvFilePath, (success) =>
        {
            if (success)
            {
                Debug.Log("CSV exported successfully.");
            }
            else
            {
                Debug.LogError("Failed to export CSV.");
            }
        });
    }
}
