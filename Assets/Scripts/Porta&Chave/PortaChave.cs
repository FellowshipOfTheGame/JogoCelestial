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
    private GameObject porta;
    Animator animator;
    AnimatorStateInfo stateInfo;


    void Start()
    {
        //pega componentes da porta(precisa estar com nome "Porta")
        porta = transform.Find("Porta").gameObject;
        animator = porta.GetComponent<Animator>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }


    private bool isFechando = false; //true se a porta ta fechando
    private bool isAcabouFechar = false; //ve se acabou a animacao "Fechando"
    void FixedUpdate()
    {
        //se pegar todas as chaves, comeca a animacao de fechar
        if(nChavesPegos == nChaves && !isFechando)
        {
            isFechando = true;
            StartCoroutine(WaitAnimation());
        }

        //ve se acabou a animacao
        if(isFechando)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAcabouFechar = stateInfo.normalizedTime >= 1 && stateInfo.IsName("Fechando");
        }
    }


    private IEnumerator WaitAnimation()
    {
        //comeca a animacao do "Fechando"
        animator.SetBool("isFechar", true);

        //espera ate acabar a animacao 
        yield return new WaitWhile(() => !isAcabouFechar);

        //apaga a porta
        porta.SetActive(false);
    }
}
