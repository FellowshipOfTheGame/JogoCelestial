using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public int health;
    public static GameManager gm;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
