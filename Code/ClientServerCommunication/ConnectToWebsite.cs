using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToWebsite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Opens the registration HTML page that is located on the Heroku server when called.
    /// </summary>
    public void Registration()
    {
        Application.OpenURL("http://cheapdell.ddns.net:25567/static/registration/index.html");
    }

    /// <summary>
    /// Opens the leaderboard HTML page that is located on the Heroku server when called.
    /// </summary>
    public void Leaderboard()
    {
        Application.OpenURL("http://cheapdell.ddns.net:25567/static/leaderboard/index.html");
    }

    /// <summary>
    /// Opens the leaderboard HTML page on the Heroku server and ends the game process.
    /// </summary>
    public void EndGameAndHeadToLeaderboard()
    {
        Application.OpenURL("http://cheapdell.ddns.net:25567/static/leaderboard/index.html");
        Application.Quit();
    }
}
