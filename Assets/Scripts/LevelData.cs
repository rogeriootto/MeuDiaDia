using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{

    public static int howManyCorrect;
    public static int howManyWrong;
    public static int howManyHints;
    public static int howManyTries;

    public static DateTime startTime;
    public static DateTime endTime;

    public static bool isPiece1Connected;
    public static bool isPiece2Connected;
    public static bool isPiece3Connected;
    public static bool isStartCountDownOver;
    public static bool isStartCountDown;
    public static bool isRefTimeOver;

    public static float timeSpentOnLevel;
    public static float hintTimeSpent;
    public static float refTimeSpentOnLevel;

    public GameObject piecesGroup;
    public GameObject timerText;
    public GameObject refPieces;

    private static string filename = "";

    public static bool piece1 = false;
    public static bool piece2 = false;
    public static bool piece3 = false;

    private static string[] fasesGaroto = { "Level-1", "Level-2", "Level-3", "Level-4", "Level-5", "Level-6" };
    private static string[] fasesGarota = { "Level-1-Menina", "Level-2-Menina", "Level-3-Menina", "Level-4-Menina", "Level-5-Menina", "Level-6-Menina" };

    void Start()
    {
        howManyCorrect = 0;
        howManyWrong = 0;
        howManyHints = 0;
        howManyTries = 0;
        isPiece1Connected = false;
        isPiece2Connected = false;
        isPiece3Connected = false;
        isStartCountDownOver = false;
        isStartCountDown = false;
        isRefTimeOver = false;
        timeSpentOnLevel = 0f;
        hintTimeSpent = 0f;
        refTimeSpentOnLevel = GameData.Instance.selectedPlayer.refHintTime;
        startTime = DateTime.Now;
        piece1 = false;
        piece2 = false;
        piece3 = false;
    }

    void Update()
    {
        if (!isRefTimeOver)
        {
            refTimeSpentOnLevel -= Time.deltaTime;
            timerText.GetComponent<TMPro.TextMeshProUGUI>().text = Mathf.FloorToInt(refTimeSpentOnLevel + 1f).ToString();

            if (refTimeSpentOnLevel <= 0f)
            {
                isRefTimeOver = true;
                isStartCountDown = true;
                piecesGroup.SetActive(true);
                timerText.SetActive(false);
                refPieces.SetActive(false);
            }
        }
        else
        {
            if (isStartCountDown)
            {
                timeSpentOnLevel += Time.deltaTime;

                if ((!isPiece1Connected || !isPiece2Connected || !isPiece3Connected) && !piece1 && !piece2 && !piece3)
                {
                    hintTimeSpent += Time.deltaTime;

                    if (hintTimeSpent > GameData.Instance.selectedPlayer.hintTime)
                    {
                        hintTimeSpent = 0f;
                        generateHint();
                        howManyHints++;
                    }

                }

                if (howManyCorrect == 3)
                {
                    StartCoroutine(WaitAndGoToNextLevel());
                }
            }
            ifConnectedStopHint();
        }
    }

    private void generateHint()
    {
        if (!piece1 && !isPiece1Connected)
        {
            GameObject.FindWithTag("hint1").GetComponent<HintLogic>().hint = true;
            piece1 = true;
            return;
        }
        if (!piece2 && !isPiece2Connected)
        {
            GameObject.FindWithTag("hint2").GetComponent<HintLogic>().hint = true;
            piece2 = true;
            return;
        }
        if (!piece3 && !isPiece3Connected)
        {
            GameObject.FindWithTag("hint3").GetComponent<HintLogic>().hint = true;
            piece3 = true;
            return;
        }
    }

    private IEnumerator WaitAndGoToNextLevel()
    {
        yield return new WaitForSeconds(3);
        GoToNextLevel();
    }

    public static void GoToNextLevel()
    {
        endTime = DateTime.Now;
        SaveDataToCSV();
        GameData.Instance.level++;
        if (GameData.Instance.level >= 6)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else
        {
            if (GameData.Instance.selectedPlayer.showBoyOn && GameData.Instance.selectedPlayer.showGirlOn)
            {
                int rng = UnityEngine.Random.Range(0, 2);
                Debug.Log(rng);

                if (rng == 0)
                {
                    Debug.Log("Fase menino rng");
                    UnityEngine.SceneManagement.SceneManager.LoadScene(fasesGaroto[GameData.Instance.level]);
                }
                else
                {
                    Debug.Log("Fase menina rng");
                    UnityEngine.SceneManagement.SceneManager.LoadScene(fasesGarota[GameData.Instance.level]);
                }
            }
            else
            {
                if (GameData.Instance.selectedPlayer.showBoyOn)
                {
                    Debug.Log("Fase menino");
                    UnityEngine.SceneManagement.SceneManager.LoadScene(fasesGaroto[GameData.Instance.level]);
                }

                if (GameData.Instance.selectedPlayer.showGirlOn)
                {
                    Debug.Log("Fase menina");
                    UnityEngine.SceneManagement.SceneManager.LoadScene(fasesGarota[GameData.Instance.level]);
                }
            }
        }
    }

    public static void SaveDataToCSV()
    {
        filename = Path.Combine(Application.persistentDataPath, "results.csv");
        Debug.Log("Saving data to: " + filename);

        bool fileExists = File.Exists(filename);

        if (!fileExists)
        {
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                tw.WriteLine("Nome; Nível; Acertos; Erros; Tentativas; Dicas; Tempo de Início; Tempo de Fim; Tempo Gasto; Tempo de Referência; Multiplas Dicas; Personagem Garoto; Personagem Garota");
            }
        }

        using (TextWriter tw = new StreamWriter(filename, true))
        {
            tw.WriteLine($"{GameData.Instance.selectedPlayer.name}; {SceneManager.GetActiveScene().name}; " +
                $"{howManyCorrect}; {howManyWrong}; {howManyTries}; {howManyHints}; " +
                $"{startTime}; {endTime}; {timeSpentOnLevel}; {GameData.Instance.selectedPlayer.hintTime};" +
                $"{GameData.Instance.selectedPlayer.isMultipleHintsOn}; " +
                $"{GameData.Instance.selectedPlayer.showBoyOn}; {GameData.Instance.selectedPlayer.showGirlOn}");
        }

    }

    private void ifConnectedStopHint()
    {
        if (isPiece1Connected)
        {
            GameObject.FindWithTag("hint1").GetComponent<HintLogic>().hint = false;
            piece1 = false;
        }
        if (isPiece2Connected)
        {
            GameObject.FindWithTag("hint2").GetComponent<HintLogic>().hint = false;
            piece2 = false;
        }
        if (isPiece3Connected)
        {
            GameObject.FindWithTag("hint3").GetComponent<HintLogic>().hint = false;
            piece3 = false;
        }
    }

}
