using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private int startNivelBuildIndex;
    [SerializeField] private int endNivelBuildIndex;
    [SerializeField] private int inicialHealth;
    [SerializeField] private Image black;

    private int actualScene;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    //apaga todos aspectos salvados no computador
    public void ResetAll()
    {
        //reseta se da para usar grapple
        string haveGrappleKey = "haveGrapple";
        PlayerPrefs.SetInt(haveGrappleKey, 0);

        //reseta vida
        PlayerPrefs.SetInt("health", inicialHealth);
        PlayerPrefs.SetInt("coins", 0);
        GameManager.gm.health = inicialHealth;
        GameManager.gm.coins = 0;
        
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        for(int i = startNivelBuildIndex; i <= endNivelBuildIndex; i++)
        {
            SceneManager.LoadScene(i);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            //reseta item coletavel
            GameObject itens = GameObject.Find("Items");
            if(itens != null) 
            {
                Transform coinChildren = itens.GetComponentInChildren < Transform > ();
                Debug.Log(coinChildren.childCount);
                for(int j = 0; j < coinChildren.childCount; j++)
                {
                    string coinKey = "coin" + SceneManager.GetActiveScene().name + j.ToString();
                    PlayerPrefs.SetInt(coinKey, 1);
                }
            }
            //reseta item vida
            itens = GameObject.Find("Lifes");
            if(itens != null) 
            {
                
                Transform lifeChildren = itens.GetComponentInChildren < Transform > ();
                Debug.Log(lifeChildren.childCount);
                for(int j = 0; j < lifeChildren.childCount; j++)
                {
                    string lifeKey = "life" + SceneManager.GetActiveScene().name + j.ToString();
                    PlayerPrefs.SetInt(lifeKey, 1);
                }
            }

            yield return new WaitForSeconds(0.1f);
            yield return new WaitForEndOfFrame();
        }

        //volta para cena inicial
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();   
        SceneManager.LoadScene(0);

        //reseta numero de niveis passados
        DeleteLevelMemory();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Destroy(this.gameObject);
    }

    

    //apaga o numero de fases passados ate agora
    public void DeleteLevelMemory()
    {
        string key = "levelsPassed";
        PlayerPrefs.DeleteKey(key);
    }

    private void OnLevelWasLoaded()
    {
        if(SceneManager.GetActiveScene().buildIndex < startNivelBuildIndex || SceneManager.GetActiveScene().buildIndex > endNivelBuildIndex)
            black.fillAmount = 1;
        else 
            black.fillAmount = 0;
    }


}
