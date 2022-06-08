using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorChaves : MonoBehaviour
{
    private Text text = null;

    [Header("GameObject Porta&Chave")]public PortaChave sistemaChave;

    void Start()
    {
        text = GetComponent<Text>();
    }


    void Update()
    {
        text.text = sistemaChave.nChavesPegos.ToString();
    }
}
