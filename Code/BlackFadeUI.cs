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

    public void StartFade()
    {
        fadeImage.enabled = true;
    }

    public void FadeInComplete()
    {
        fadeImage.enabled = false;
    }

    public void FadeOutComplete()
    {
        SceneManager.LoadScene(loadSceneName);
    }
}
