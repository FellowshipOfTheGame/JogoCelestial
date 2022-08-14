using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemVidaSc : MonoBehaviour
{
    //ve se colidiu com player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string playerTag = "Player";
        if (collision.tag == playerTag)
        {
            Transform children = this.transform.parent.GetComponentInChildren<Transform>();

            for (int i = 0; i < children.childCount; i++)
            {
                if (children.transform.GetChild(i).gameObject.name == this.gameObject.name)
                {
                    string lifeKey = "life" + SceneManager.GetActiveScene().name + i.ToString();
                    PlayerPrefs.SetInt(lifeKey, 0);
                    GameManager.gm.health++;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
