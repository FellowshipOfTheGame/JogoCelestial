using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classes dependentes
/// LevelSelector - Colocado no Level1Menu(Cena Inicial)
///               - Cria os Botoes
/// LevelButton - Colocado no Level1Button(Cena Inicial)
///             - Quando clicado, vai para o level correspondente
/// VerifyScenePass - Colocado em cada Fase
///                 - Atualiza se passou nessa fase
/// </summary>
public class LevelButton : MonoBehaviour
{
    [HideInInspector] public int LevelNumber;

    private Fade fade;
    private bool startFade = false;
    private bool isFading = false;

    //chamado quando clica no botao
    public void GoToFase()
    {
        startFade = true;
    }

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
            SceneManager.LoadScene(LevelNumber);
        }
    }
}
