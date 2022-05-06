using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextArea : MonoBehaviour
{
    private bool encostou = false;
    private bool isfading = false;
    private Fade fade = null;

    private void Start()
    {
        GameObject fadeObj = GameObject.Find("Fade");
        fade = fadeObj.GetComponent<Fade>();
        if (fade == null) Debug.Log("aa");
    }

    void Update()
    {
        //se o player chegou no fim da fase, comeca o fadeOut
        if (encostou && !isfading)
        {
            isfading = true;
            fade.StartFadeOut();
        }

        //quando termina o fadeOut muda de fase
        if (fade.IsFadeOutComplete())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //ve se o player chegou no fim da fase
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string playerTag = "Player";
        if (collision.gameObject.tag == playerTag)
        {
            encostou = true;
        }
    }
}
