using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script relacionado
//Chave:
//-detecta se pegou chave

public class PortaChave : MonoBehaviour
{
    //Componentes Chave
    [Header("Numero de Chaves")] 
    public int nChaves; //numero de chaves total
    [HideInInspector] public int nChavesPegos = 0; //numero de chaves pegos

    //Componentes Porta
    [Header("Velocidade de abrir")]
    public float speed;
    [Header("Orientacao da Porta")]
    public bool paraCima;

    private GameObject porta;
    private Rigidbody2D body;


    void Start()
    {
        //pega componentes da porta(precisa estar com nome "Porta")
        porta = transform.Find("Porta").gameObject;
        body = porta.GetComponent<Rigidbody2D>();
    }


    private bool isAbrindo = false; //true se a porta ta abrindo
    void FixedUpdate()
    {
        if(nChavesPegos == nChaves && !isAbrindo)
        {
            Debug.Log("a");
            isAbrindo = true;
            StartCoroutine(OpenAnimation());
        }
    }


    private IEnumerator OpenAnimation()
    {
        if (paraCima)
            body.velocity = new Vector2(0, speed);
        else
            body.velocity = new Vector2(0, -speed);
        yield return new WaitForSeconds(3 / speed);
        porta.SetActive(false);
    }
}
