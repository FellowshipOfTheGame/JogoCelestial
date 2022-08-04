using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadFase : MonoBehaviour
{
    DamageSystem damSys = null;
    Fade fade = null;

    private bool fading = false;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        damSys = player.GetComponent<DamageSystem>();

        GameObject fadeObj = GameObject.Find("Fade");
        fade = fadeObj.GetComponent<Fade>();
    }


    void Update()
    {
        if(damSys.isDied && !fading)
        {
            fading = true;
            fade.StartFadeOut();
        }
        if(fade.IsFadeOutComplete() && fading)
        {
            if( GameManager.gm.health>0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else
                SceneManager.LoadScene("Assets/Scenes/Levels/TelaGameOver.unity");
        }
    }
}
