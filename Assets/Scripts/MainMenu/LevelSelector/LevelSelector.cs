using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classes dependentes
/// LevelSelector - Colocado no Level1Menu(Cena Inicial)
///               - Cria os Botoes
/// LevelButton - Colocado no Level1Button(Cena Inicial)
///             - Quando clicado, vai para o level correspondente
/// VerifyScenePass - Colocado em cada Fase
///                 - Atualiza se passou nessa fase
/// </summary>
public class LevelSelector : MonoBehaviour
{
    public int levelCount;
    public GameObject ButtonPrehub;
    [Header("Numero maximo de botoes em horizontal")] public int maxXButton;
    [Header("Numero maximo de botoes em vertical")] public int maxYButton;
    [Header("Espaco entre botoes em x")] public int XDeslocation;
    [Header("Espaco entre botoes em y")] public int YDeslocation;

    private Vector3 level1Pos = new Vector3();


    void Start()
    {
        //carrega quantas levels foram passados ate agora
        int levelsPassed = 1;
        string key = "levelsPassed";
        if (PlayerPrefs.HasKey(key))
            levelsPassed = PlayerPrefs.GetInt(key, 1);
        else
        {
            PlayerPrefs.SetInt(key, 1);
        }
        PlayerPrefs.Save();


        //pega o objeto do Level1Button
        GameObject level1Button = transform.Find("Level1Button").gameObject;
        level1Pos = level1Button.transform.position;


        ///cria os botoes
        int levelCounter = 0;
        for (int i = 0; i < maxXButton; i++)
        {
            for(int j = 0; j < maxYButton; j++)
            {
                levelCounter++;
                if (levelCounter > levelCount)
                    break;

                //cria o botao
                GameObject button = Instantiate(ButtonPrehub);
                button.transform.parent = this.transform;
                button.transform.position = new Vector3(level1Pos.x + i * XDeslocation, level1Pos.y - j * YDeslocation, 0);

                Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
                buttonText.text = "Level " + levelCounter;

                LevelButton lb = button.GetComponent<LevelButton>();
                lb.LevelNumber = levelCounter;

                //seta se pode entrar nessa fase ou nao
                //se entrou uma vez nessa fase, pode entrar
                if (levelsPassed < levelCounter)
                    button.GetComponent<Button>().enabled = false;
            }
        }
        level1Button.SetActive(false);

    }

    //usado para debugar
    //apaga o numero de fases passados ate agora
    public void DeleteLevelMemory()
    {
        string key = "levelsPassed";
        PlayerPrefs.DeleteKey(key);
    }
}
