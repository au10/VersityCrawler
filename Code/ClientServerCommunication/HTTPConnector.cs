using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using UnityEngine.Networking;
using TMPro;

public class HTTPCOnnector : MonoBehaviour
{
    public PlayerData PlayerData { get; set; }
    public static HTTPCOnnector instance;
    public TextMeshProUGUI errorText;

    private void Awake()
    {
        instance = this;
        PlayerData = FindObjectOfType<PlayerData>();
        errorText = GameObject.Find("Error").GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0f;
        errorText.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// This method creates a client side POST request that is sent to the HTTP server located on
    /// Heroku. It converts the json to bytes and then sends it to the server. The JSON is converted to bytes so that
    /// the server language (Python) can translate it. If the server sends the correct message, the player can start the game.
    /// If not, then it prompts the player with a safe error message
    /// </summary>
    /// <param name="url">the url to the HTTP server</param>
    /// <param name="json">the json file to send to the server</param>
    /// <returns>the web request</returns>
    public IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();
        Debug.Log("Received: " + uwr.downloadHandler.text);
        if (uwr.downloadHandler.text == "\"Login success!\"")
        {
            GameObject.Find("Registration").SetActive(false);
            //Time.timeScale = 1f;
        }
        else
        {
            errorText.enabled = true;
            errorText.SetText(uwr.downloadHandler.text);
        }
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
