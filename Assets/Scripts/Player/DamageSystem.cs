using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [HideInInspector] public bool isDied = false; //vira true quando morre
    [HideInInspector] public bool isDying = false; //vira true acerta no espinho ou algo
    private bool isAcabouDeadAnim =false;

    private PlayerMovement plMove = null;
    private Animator anime = null;
    private AnimatorStateInfo stateInfo;
    
    [Header("FMOD Sound:")]
    public GameObject menu;
    public float FX;

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
            GameManager.gm.health -=1;
            Debug.Log("Vida Restante: " + GameManager.gm.health);

            plMove._isDead = true;
            isDying = true;
            anime.Play("death");
            PlayOneShot("event:/Jogo Celestial/SFX/death");
        }
    }

    //funcao parab conseguir determinar o volume dos efeitos sonoros
    public void PlayOneShot(string path){
        FMOD.Studio.EventInstance soundFX = FMODUnity.RuntimeManager.CreateInstance(path);
        menu = GameObject.FindWithTag ("Canvas");
        FX = menu.transform.GetComponent<SettingsMenu>().volumeFX;
        soundFX.setVolume(FX);
        soundFX.start();
        soundFX.release();
    }
}
