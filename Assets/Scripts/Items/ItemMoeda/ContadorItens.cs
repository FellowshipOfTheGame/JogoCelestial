using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorItens : MonoBehaviour
{
    private Text text = null;
    private GameObject item;
    Transform coinChildren;

    void Start()
    {
        text = GetComponent<Text>();
        item = GameObject.Find("Items");
        coinChildren = item.GetComponentInChildren < Transform > ();
    }


    void Update()
    {
        int coinCounter = 0;

         
        for(int j = 0; j < coinChildren.childCount; j++)
        {
            if(!item.transform.GetChild(j).gameObject.active)
            {
                coinCounter++;
            }
        }

        text.text =(coinCounter + "/" + coinChildren.childCount);
    }
}
