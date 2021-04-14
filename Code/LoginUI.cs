using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField Username;
    public TMP_InputField Password;
    public PlayerData PlayerData { get; set; }

    private void Awake()
    {
        Time.timeScale = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerData = FindObjectOfType<PlayerData>();
        PlayerData.PlayerLogin = new PlayerLogin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUserNamePassword()
    {
        PlayerData.PlayerLogin.username = Username.text;
        PlayerData.PlayerLogin.password = Password.text;
        string json = JsonUtility.ToJson(PlayerData.PlayerLogin);
        StartCoroutine(HTTPCOnnector.instance.PostRequest("http://cheapdell.ddns.net:25567/login", json));
        
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        UnPause();
        Destroy(GameObject.Find("InstructionsUI"));
    }
}
