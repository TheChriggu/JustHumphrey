using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartScreenButton : MonoBehaviour
{
    public float fadeSpeed = 1;
    bool isFadingIn = false;
    Image button;
    public TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(text.color.a >= 1 && isFadingIn)
        {
            isFadingIn = false;
        }
        if(text.color.a <= 0 && !isFadingIn)
        {
            isFadingIn = true;
        }

        if(isFadingIn)
        {
            fadeIn();
        }
        else
        {
            fadeOut();
        }
    }

    public void OnButtonDown()
    {
        //GameStateTracker.IsNewGame = false;
        SceneManager.LoadScene("UI");
    }

    void fadeIn()
    {
        Color buttonColor = button.color;
        buttonColor.a += fadeSpeed * 0.01f;
        button.color = buttonColor;

        Color textColor = text.color;
        textColor.a += fadeSpeed * 0.01f;
        text.color = textColor;
    }

    void fadeOut()
    {
        Color buttonColor = button.color;
        buttonColor.a -= fadeSpeed * 0.01f;
        button.color = buttonColor;

        Color textColor = text.color;
        textColor.a -= fadeSpeed * 0.01f;
        text.color = textColor;
    }
}
