using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemVidaController : MonoBehaviour
{
    //usado para verificar status dos itens vida
    private static int naoCadastrado = -1;
    private static int tem = 1;
    private static int naoTem = 0;

    void Start()
    {
        Transform children = this.GetComponentInChildren<Transform>();

        for (int i = 0; i < children.childCount; i++)
        {
            
            string lifeKey = "life" + SceneManager.GetActiveScene().name + i.ToString();
            int lifeStatus = PlayerPrefs.GetInt(lifeKey, -1);
            if (lifeStatus == tem)
                this.transform.GetChild(i).gameObject.SetActive(true);
            else if (lifeStatus == naoTem)
                this.transform.GetChild(i).gameObject.SetActive(false);
            else
                PlayerPrefs.SetInt(lifeKey, 1);
        }
    }
}
