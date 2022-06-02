using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Utilizacao
//Coloca true nos variaveis que voce quer salvar
//Chame a funcao SaveScene quando quer salvar os dados
//e a funcao LoadScene quando quer carregar os dados


//Funcionamento
//SceneSaveVariables - pega todos variaveis que quer salvar do player
//                    - SaveScene - junta e manda dados para SaveSystem
//                    - LoadScene - pega dados do SaveSystem e coloca em outros Scripts
//
//SaveSystem - SaveScene - pega dados da cena e coloca num file(para conseguir salvar)
//           - LoadScene - pega dados do file e da o retorno
//
//SceneData - funciona tipo um variavel, que contem todos tipos de dados que quer salvar

public class SceneSaveVariables : MonoBehaviour
{
    //bools(serao falsos caso nao quer salvar variavel nessa cena)
    [Header("Parametros que vao ser salvos")]
    public bool isColectableItem;


    //variaveis que precisam ser salvos
    [HideInInspector] public bool[] isExistItem;
    private string itensName = "Items";



    //pega Componentes
    [HideInInspector] public List<GameObject> items = new List<GameObject>();
    private void Start()
    {
        //item coletavel
        Transform itemParentTransform;
        if (isColectableItem)
        {
            itemParentTransform = GameObject.Find(itensName).transform;
            foreach (Transform child in itemParentTransform)
            {
                items.Add(child.gameObject);
            }
        }

        LoadScene();
    }


    //pega os variaveis de outros scripts
    //e manda o Script SaveSystem salvar as variaveis
    public void SaveScene()
    {
        //parametros do coletavel
        isExistItem = new bool[items.Count];
        if (isColectableItem)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].activeSelf)
                    isExistItem[i] = true;
                else 
                    isExistItem[i] = false;
            }
        }
        else isExistItem = null;


        //manda dados para SaveSystem
        SaveSystem.SaveScene(this);
    }


    //pega dados salvos e coloca em cada Script
    public void LoadScene()
    {
        //pega dados do SaveSystem
        SceneData data = SaveSystem.LoadScene();

        //parametros do coletavel
        if (isColectableItem)
        {
            int nItems = data.isExistItem.Length;
            for (int i = 0; i < nItems; i++)
            {
                items[i].SetActive(data.isExistItem[i]);
            }
        }
    }
}
