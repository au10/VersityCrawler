using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void NewGame()
    {
        Animator anim = GameObject.Find("BlackFade").GetComponent<Animator>();
        anim.SetTrigger("Fade");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
