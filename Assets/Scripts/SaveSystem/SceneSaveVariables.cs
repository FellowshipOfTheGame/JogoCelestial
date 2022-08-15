using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaveVariables : MonoBehaviour
{
    //usado para verificar status dos itens
    private static int naoCadastrado = -1;
    private static int tem = 1;
    private static int naoTem = 0;

    void Start()
    {
        Transform children = this.GetComponentInChildren<Transform>();

        for (int i = 0; i < children.childCount; i++)
        {
            
            string coinKey = "coin" + SceneManager.GetActiveScene().name + i.ToString();
            int coinStatus = PlayerPrefs.GetInt(coinKey, -1);
            if (coinStatus == tem)
                this.transform.GetChild(i).gameObject.SetActive(true);
            else if (coinStatus == naoTem)
                this.transform.GetChild(i).gameObject.SetActive(false);
            else
                PlayerPrefs.SetInt(coinKey, 1);
        }
    }
}
