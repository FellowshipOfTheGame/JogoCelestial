using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : MonoBehaviour
{
    [HideInInspector] public bool isDied = false; //vira true quando morre
    [HideInInspector] public bool isDying = false; //vira true acerta no espinho ou algo
    private bool isAcabouDeadAnim =false;

    private PlayerMovement plMove = null;
    private Animator anime = null;
    private AnimatorStateInfo stateInfo;

    private void Start()
    {
        plMove = this.gameObject.GetComponent<PlayerMovement>();
        anime = this.gameObject.GetComponent<Animator>();
    }


    private void Update()
    {
        if(isDying)
        {
            stateInfo = anime.GetCurrentAnimatorStateInfo(0);
            isDied = stateInfo.normalizedTime >= 1 && stateInfo.IsName("death");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string damageAreaTag = "DamageArea";
        if (collision.tag == damageAreaTag)
        {
            plMove._isDead = true;
            isDying = true;
            anime.Play("death");
        }
    }
}
