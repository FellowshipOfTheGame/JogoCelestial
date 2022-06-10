using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSc : MonoBehaviour
{
    private Text text;
    private string originalText;

    void OnEnable()
    {
        text.text = originalText;
    }
    private void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>();
        originalText = text.text;
    }
    public void OnPointerEnterEvent()
    {
        text.text = "< " + originalText + " >";
    }
    public void OnPointerExitEvent()
    {
        text.text = originalText;
    }
}
