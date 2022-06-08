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
public class VerifyScenePass : MonoBehaviour
{
    void Start()
    {
        //se for primeira vez que entra nessa fase,
        //atualiza quantas level foram passados
        string key = "levelsPassed";
        int levelsPassed = PlayerPrefs.GetInt(key);
        if (levelsPassed < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt(key, SceneManager.GetActiveScene().buildIndex);
        }

        Destroy(this.gameObject);
    }
}
