using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
//This class is used to send JSON data to HTTP server when game is completed
public class GameStatsManager
{
    public string username;
    public float time;
    public int level;

    public GameStatsManager(string username, float time)
    {
        this.username = username;
        this.time = time;
        level = 1;
    }
}
