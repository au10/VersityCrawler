using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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
