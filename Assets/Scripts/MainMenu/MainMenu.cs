using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Fade fade;
    private bool startFade = false;
    private bool isFading = false;

    private void Start()
    {
        GameObject fadeObj = GameObject.Find("Fade");
        fade = fadeObj.GetComponent<Fade>();
    }

    private void Update()
    {
        if (startFade && !isFading)
        {
            isFading = true;
            fade.StartFadeOut();
        }
        if (fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Play()
    {
        startFade = true;
    }


    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
