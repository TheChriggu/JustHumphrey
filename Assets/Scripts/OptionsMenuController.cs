using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
    public RestartDialogue restartDialogue;
    bool isOpen = false;

    public AudioSource BackgroundMusic;
    public List<AudioSource> SFX;

    public void OpenOptions()
    {

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        isOpen = true;
    }

    public void CloseOptions()
    {
        if(!restartDialogue.GetIsOpen())
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            isOpen = false;        
        }

    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

    public void ToggleMusic()
    {
        if (BackgroundMusic.enabled)
        {
            BackgroundMusic.enabled = false;
        }
        else
        {
            BackgroundMusic.enabled = true;
        }
    }

    public void ToggleSFX()
    {
        if (SFX[0].enabled)
        {
            foreach(AudioSource audio in SFX)
            {
                audio.enabled = false;
            }
        }
        else
        {
            foreach(AudioSource audio in SFX)
            {
                audio.enabled = true;
            }
        }
    }
}
