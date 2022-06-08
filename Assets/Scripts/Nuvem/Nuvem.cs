using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuvem : MonoBehaviour
{
    [Header("Tempo para nuvem desaparecer")] public float desappearTime;
    [Header("Tempo para nuvem voltar")] public float regenerationTime;

    //componentes desse gameObject
    private BoxCollider2D col2d;
    private SpriteRenderer sprite;
    private Animator anim;

    void Start()
    {
        //pega componenetes do gameObject
        col2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //quando o player colide
        string playerTag = "Player";
        if (collision.gameObject.tag == playerTag)
        {
            //ve se player ta cima da nuvem
            BoxCollider2D playerCol = collision.gameObject.GetComponent<BoxCollider2D>();
            float playerPos = playerCol.transform.position.y - (playerCol.size.y / 2);
            float cloudPos = col2d.transform.position.y + (col2d.size.y / 2);
            bool isPlayerUp = playerPos >= cloudPos;

            if (isPlayerUp)
            {
                //espera antes de perder a fisica
                StartCoroutine(WaitDisappear());
            }
        }
    }

    private IEnumerator WaitDisappear()
    {
        //espera antes de perder a fisica
        yield return new WaitForSeconds(desappearTime);

        //comeca a animacao de sumir
        anim.SetBool("sumir", true);

        //tira o collider da nuvem
        col2d.enabled = false;

        //espera antes de regenerar
        StartCoroutine(WaitRegeneration());
    }

    private IEnumerator WaitRegeneration()
    {
        //espera antes de regenerar
        yield return new WaitForSeconds(regenerationTime);

        //comeca a animacao de reaparecer
        anim.SetBool("sumir", false);

        //coloca o collider na nuvem
        col2d.enabled = true;
    }
}
