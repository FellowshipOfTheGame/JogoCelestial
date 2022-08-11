using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Fade fade;
    private bool startFade = false;
    private bool isFading = false;
    public GameObject pauseGame;
    public bool isPaused;
    public bool isBackToMenu = false;

    [SerializeField] private GameObject OptionMenu;

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
        if (startFade && fade.IsFadeOutComplete())
        {
            if(!isBackToMenu)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else
                SceneManager.LoadScene(0);
        }
    }

    public void Play()
    {
        startFade = true;
    }

    /*pausa o jogo, se tiver pausado volta para o jogo*/
    public void PauseScreen(){
        if(isPaused && !OptionMenu.activeSelf){
            isPaused = false;
            Time.timeScale = 1f;
            pauseGame.SetActive(false);
        }else if(!OptionMenu.activeSelf){           
            isPaused = true;
            Time.timeScale = 0f;
            pauseGame.SetActive(true);
        }
    }

    public void Continue(){
        isPaused = false;
        Time.timeScale = 1f;
        pauseGame.SetActive(false);
    }

    public void backToMainMenu(){
        startFade = true;
        isBackToMenu = true;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    //apaga o numero de fases passados ate agora
    public void DeleteLevelMemory()
    {
        string key = "levelsPassed";
        PlayerPrefs.DeleteKey(key);
    }
}
