using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaChave : MonoBehaviour
{
    //Componentes Chave
    [Header("Numero de Chaves")] 
    public int nChaves; //numero de chaves total
    [HideInInspector] public int nChavesPegos = 0; //numero de chaves pegos

    //Componentes Porta
    private GameObject porta; //gameObject da porta


    void Start()
    {
        //pega o gameObject da porta(precisa estar com nome "Porta")
        porta = transform.Find("Porta").gameObject;
    }


    void Update()
    {
        //se pegar todas as chaves, apaga a porta
        if(nChavesPegos == nChaves)
        {
            porta.SetActive(false);
        }
    }
}
