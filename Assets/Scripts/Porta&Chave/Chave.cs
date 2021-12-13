using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    private PortaChave portaChave;
    void Start()
    {
        GameObject parente = transform.parent.gameObject;
        portaChave = parente.GetComponent<PortaChave>();

        //debug
        if (portaChave == null) 
            Debug.Log("Verifique se tem script PortaChave no parente desse gameObject");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string playerTag = "Player";
        if (collision.tag == playerTag)
        {
            portaChave.nChavesPegos++;
            this.gameObject.SetActive(false);
        }
    }
}
