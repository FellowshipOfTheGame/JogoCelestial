using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader instance;

    [SerializeField] private Animator transition;

    [SerializeField] private float transitionTime;

    // temporario
    private float tempoMorte = 5f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Mais de 1 level loader");
        }
        else
        {
            instance = this;
        }
    }

    public static LevelLoader GetInstance()
    {
        return instance;
    }

    void Update()
    {
        // temporario
        if(tempoMorte <= float.Epsilon)
        {
            ReloadScene();
        }
        else 
        {
            tempoMorte -= Time.deltaTime;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadTransition(sceneName));
    }

    public void ReloadScene()
    {
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().name));
    }

    private IEnumerator LoadTransition(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
