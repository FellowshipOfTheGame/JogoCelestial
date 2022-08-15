using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public int health;
    public int coins;
    public static GameManager gm;

    private int oldHealth = 0;
    private int oldCoins = 0;

    // Start is called before the first frame update

    void Awake () {


        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);

	}

    void Start()
    {
        health = PlayerPrefs.GetInt("health", health);
        coins = PlayerPrefs.GetInt("coins", coins);
    }

    // Update is called once per frame
    void Update()
    {
        if(coins >= 10){
            health++;
            coins = 0;
        }

        Save();
    }

    private void Save()
    {
        if(health != oldHealth)
        {
            PlayerPrefs.SetInt("health", health);
            oldHealth = health;
        }
        if(coins != oldCoins)
        {
            Debug.Log(coins);
            PlayerPrefs.SetInt("coins", coins);
            oldCoins = coins;
        }
    }


    
}
