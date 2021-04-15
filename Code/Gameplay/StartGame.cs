using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    /// <summary>
    /// When start game button is pressed, start the UI "BlackFade"
    /// </summary>
    public void NewGame()
    {
        Animator anim = GameObject.Find("BlackFade").GetComponent<Animator>();
        anim.SetTrigger("Fade");
    }

    /// <summary>
    /// Kill the game program
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
