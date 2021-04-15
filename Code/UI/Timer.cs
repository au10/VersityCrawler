using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static float secondTicker;
    public static float numSeconds;
    public static int minuteTicker;

    public bool gameEnded;
    public bool gameStarted;

    public GameObject endGameUI;

    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        endGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            if (secondTicker >= 60)
            {
                minuteTicker++;
                secondTicker = 0;
            }
            secondTicker += Time.deltaTime;
            numSeconds += Time.deltaTime;
            if (minuteTicker == 0)
            {
                timerText.SetText(string.Format("{0:0.00}", secondTicker));
            }
            else
            {
                timerText.SetText(minuteTicker + ":" + string.Format("{0:00.00}", secondTicker));
            }
        }
        else
        {
            //EndGame();
            //gameEnded = false;
        }
    }

    public void EndGame()
    {
        Debug.Log("Sending info");
        Time.timeScale = 0;
        GameStatsManager gameStatsManager = new GameStatsManager(FindObjectOfType<PlayerData>().PlayerLogin.username, numSeconds);
        string json = JsonUtility.ToJson(gameStatsManager);
        StartCoroutine(HTTPCOnnector.instance.PostRequest("http://cheapdell.ddns.net:25567/leaderboard", json));
        endGameUI.SetActive(true);
        endGameUI.GetComponentInChildren<TextMeshProUGUI>().SetText("Your time was " + string.Format("{0:00.00}", numSeconds) + " seconds!");
    }
}
