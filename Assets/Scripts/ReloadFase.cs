using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadFase : MonoBehaviour
{
    DamageSystem damSys = null;
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        damSys = player.GetComponent<DamageSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damSys.isDied)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
