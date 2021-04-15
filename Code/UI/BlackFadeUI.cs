using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackFadeUI : MonoBehaviour
{
    public Animator fadeAnim;
    private Image fadeImage;
    public string loadSceneName;

    // Start is called before the first frame update
    void Start()
    {
        fadeAnim = GetComponent<Animator>();
        fadeImage = GetComponent<Image>();
    }

    /// <summary>
    /// Called when the object needs to start fade animation
    /// </summary>
    public void StartFade()
    {
        fadeImage.enabled = true;
    }

    /// <summary>
    /// Called after the screen is completely clear and the BlackFade is transparent
    /// </summary>
    public void FadeInComplete()
    {
        fadeImage.enabled = false;
    }

    /// <summary>
    /// Called when the screen is completely black from the BlackFadeUI
    /// </summary>
    public void FadeOutComplete()
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
