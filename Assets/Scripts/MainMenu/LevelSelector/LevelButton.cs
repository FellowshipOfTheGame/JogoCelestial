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

    //chamado quando clica no botao
    public void GoToFase()
    {
        SceneManager.LoadScene(LevelNumber);
    }
}
