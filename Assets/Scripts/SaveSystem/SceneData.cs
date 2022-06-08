using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    //variaveis da cena que deverao ser salvos
    [HideInInspector] public bool[] isExistItem;

    //pega dados e junta no variavel tipo "SceneData"
    public SceneData(SceneSaveVariables scene)
    {
        //parametros do coletavel
        int nItems = scene.isExistItem.Length;
        isExistItem = new bool[nItems];
        for(int i = 0; i < nItems; i++)
        {
            isExistItem[i] = scene.isExistItem[i];
        }
    }
}