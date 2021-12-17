using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script relacionado
//PortaChave:
//-controla a sistema de porta com chave

public class Chave : MonoBehaviour
{
    //script PortaChave do parente
    private PortaChave portaChave;

    void Start()
    {
        //pega script PortaChave do parente
        GameObject parente = transform.parent.gameObject;
        portaChave = parente.GetComponent<PortaChave>();

        //debug
        if (portaChave == null) 
            Debug.Log("Verifique se tem script PortaChave no parente desse gameObject");
    }


    //detecta a colisao com Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string playerTag = "Player";
        if (collision.tag == playerTag)
        {
            //aumenta numero de chaves pegos
            portaChave.nChavesPegos++;

            //apaga esse gameObject
            this.gameObject.SetActive(false);
        }
    }
}
