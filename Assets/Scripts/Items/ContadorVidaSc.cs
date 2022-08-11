using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorVidaSc : MonoBehaviour
{
    GameManager gameManager;
    Text text;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (gameManager == null) Debug.Log("Cannot find game manager");

        text = GetComponent<Text>();
        text.text = gameManager.health.ToString();
    }
}
