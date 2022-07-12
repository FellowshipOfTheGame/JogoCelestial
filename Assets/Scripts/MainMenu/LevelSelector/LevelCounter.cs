using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCounter : MonoBehaviour
{
    private Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Level " + (SceneManager.GetActiveScene().buildIndex);
    }
}
