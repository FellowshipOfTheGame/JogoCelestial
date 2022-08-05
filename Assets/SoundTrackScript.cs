using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundTrackScript : MonoBehaviour
{

    public FMOD.Studio.EventInstance gameSoundtrack;
    public FMOD.Studio.EventInstance menuSoundtrack;
    public string menuScenePath;
    public string menuSceneName;

    private string sceneName;

    public GameObject menu;
    public float Master;
    public float Music;
    public float FX;

    // Start is called before the first frame update
    public void Start()
    {
        sceneName = "Level 1";
        gameSoundtrack = FMODUnity.RuntimeManager.CreateInstance("event:/Jogo Celestial/Trilha/Trilha_Celestimiau2D");
        menuSoundtrack = FMODUnity.RuntimeManager.CreateInstance("event:/Jogo Celestial/Trilha/Menu_Celestimiau");
        menuSoundtrack.start();
        menuSoundtrack.release();
        gameSoundtrack.start();
        gameSoundtrack.release();
        menuSoundtrack.setPaused(true);
        gameSoundtrack.setPaused(true);
        
        menuSoundtrack.setPaused(false);

        Debug.Log("comecooouuuu");
    }

    // Update is called once per frame
    void Update()
    {   
        //atualizar e armazenar os valores para os sons
        menu = GameObject.FindWithTag ("Canvas");
        FX = menu.transform.GetComponent<SettingsMenu>().volumeFX;
        Music = menu.transform.GetComponent<SettingsMenu>().volumeMusic;
        Master = menu.transform.GetComponent<SettingsMenu>().volumeMaster;
        
        CheckSoundtrackChange();
        gameSoundtrack.setVolume(Music);
        menuSoundtrack.setVolume(Music);
    }


    void CheckSoundtrackChange()
    {
        var currentScene = SceneManager.GetActiveScene();
        var currentScenename = currentScene.name;
        if (!string.Equals(currentScenename, sceneName)){
            //se est� saindo do menu, troca para a trilha do jogo
            if(string.Equals("StartFase(Test)", sceneName))
            {
                Debug.Log("Saiu do Menu!");
                menuSoundtrack.setPaused(true);
                gameSoundtrack.setPaused(false);
            }
            //se est� voltando para o menu, troca para a trilha do menu
            else if(string.Equals("StartFase(Test)", currentScenename))
            {
                Debug.Log("Voltou para o menu!");
                gameSoundtrack.setPaused(true);
                menuSoundtrack.setPaused(false);

            }
            Debug.Log(sceneName + "-->" + currentScenename);
            sceneName = currentScenename;
        }
    }
}
