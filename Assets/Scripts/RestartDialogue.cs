using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartDialogue : MonoBehaviour
{
    bool isOpen = false;
    public void OpenDialogue()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        isOpen = true;
    }

    public void CloseDialogue()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        isOpen = false;
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }

}
