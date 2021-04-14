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

    public void Registration()
    {
        Application.OpenURL("http://cheapdell.ddns.net:25567/static/registration/index.html");
    }

    public void Leaderboard()
    {
        Application.OpenURL("http://cheapdell.ddns.net:25567/static/leaderboard/index.html");
    }

    public void EndGameAndHeadToLeaderboard()
    {
        Application.OpenURL("http://cheapdell.ddns.net:25567/static/leaderboard/index.html");
        Application.Quit();
    }
}
