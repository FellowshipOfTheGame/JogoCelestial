using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio:")]
    public float volumeMaster;
    public float volumeFX;
    public float volumeMusic;
    public Slider Music;
    public Slider Effects;
    public Slider Master;

    [Header("Resolution:")]
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    
    private void Start(){
        
        //limpar o botao Dropdown
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //criar uma lista de opcoes
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        //adicionar na lista as opcoes de resolucoes
        for(int i = 0; i<resolutions.Length; i++){
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        
        //adicionar as opcoes de resolucao o botao Dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void Update(){
        //alterar o valor do slider de musica e efeitos sonoros, quando o volumeMaster mudar
        Music.value = volumeMusic;
        Effects.value = volumeFX;

        if(volumeMusic == volumeFX)
            Master.value = volumeMusic;
    }

    //configura o volume de todos os sons do jogo
    public void VolumeMaster(float volume){
        volumeMaster = volume;
        
        //alterar o volume da trilha sonora e efeitos
        volumeFX = volume;
        volumeMusic = volume;
    }

    //configura o volume dos efeitos sonoros do jogo
    public void VolumeFX(float volume){
        volumeFX = volume;
    }

    //configura o volume das musicas do jogo
    public void VolumeMusic(float volume){
        volumeMusic = volume;
    }

    //mudar qualidade grafica
    public void SetQuality(int qualityIndex){
        Debug.Log(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //colocar ou sair da tela cheia
    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }

    //mudar resolucao da tela
    public void SetResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
