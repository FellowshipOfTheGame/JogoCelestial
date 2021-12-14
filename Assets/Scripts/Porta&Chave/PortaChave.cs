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


    private bool isAbrindo = false; //true se a porta ta abrindo
    private bool isAcabouAbrir = false; //ve se acabou a animacao "Abrindo"
    void FixedUpdate()
    {
        //se pegar todas as chaves, comeca a animacao de abrir
        if(nChavesPegos == nChaves && !isAbrindo)
        {
            isAbrindo = true;
            StartCoroutine(WaitAnimation());
        }

        //ve se acabou a animacao
        if(isAbrindo)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            isAcabouAbrir = stateInfo.normalizedTime >= 1 && stateInfo.IsName("Abrindo");
        }
    }


    private IEnumerator WaitAnimation()
    {
        //comeca a animacao do "Abrindo"
        animator.SetBool("isFechar", true);

        //espera ate acabar a animacao 
        yield return new WaitWhile(() => !isAcabouAbrir);

        //apaga a porta
        porta.SetActive(false);
    }
}
