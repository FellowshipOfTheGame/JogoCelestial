using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorItens : MonoBehaviour
{
    private Text text = null;
    private SceneSaveVariables saveVar;

    void Start()
    {
        text = GetComponent<Text>();
        
        GameObject saveSys = GameObject.Find("SceneSaveSystem");
        saveVar = saveSys.GetComponent<SceneSaveVariables>();
    }


    void Update()
    {
        int itemCounter = 0;
        foreach(GameObject itemI in saveVar.items)
        {
            if (itemI.activeSelf)
                itemCounter++;
        }
        
        text.text =(saveVar.items.Count-itemCounter).ToString() + "/" + saveVar.items.Count.ToString();
    }
}
