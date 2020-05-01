using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueInterface : MonoBehaviour
{ 
    public GameObject HumphreyTextbox;
    public GameObject AnneTextbox;
    public GameObject DottieTextbox;
    public GameObject MadsTextbox;
    public GameObject MadMadsTextbox;
    public GameObject OliveTextbox;
    public GameObject SiggieTextbox;
    public GameObject InteractableTextbox;
    public GameObject ArrowForward;
    public GameObject ArrowBackward;
    public enum ArrowsToShow{Forward, Backward, Both};

    DialogueNodes.Dialogue dialogue;

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    OnForwardButtonDown();
                }
            }
        }
#endif
#if !UNITY_ANDROID
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                OnForwardButtonDown();
            }
        }
#endif
    }

    public void SetText(string NewText, CharacterNames name)
    {

        switch (name)
        {
            case CharacterNames.Hamster:
                HumphreyTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.Anne:
                AnneTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.Dottie:
                DottieTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.Mads:
                MadsTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.MadMads:
                MadMadsTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.Olive:
                OliveTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.Siggie:
                SiggieTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
            case CharacterNames.Interactable:
                InteractableTextbox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = NewText;
                break;
        }
    }

    public void ShowText(CharacterNames name, ArrowsToShow arrows)
    {
        HideText();

        switch (name)
        {
            case CharacterNames.Hamster:
                HumphreyTextbox.SetActive(true);
                GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().Talk();
                break;   
            case CharacterNames.Anne:
                AnneTextbox.SetActive(true);
                break;          
            case CharacterNames.Dottie:
                DottieTextbox.SetActive(true);
                break;   
            case CharacterNames.Mads:                
                MadsTextbox.SetActive(true);
                break;   
            case CharacterNames.MadMads:                
                MadMadsTextbox.SetActive(true);
                break;    
            case CharacterNames.Olive:                
                OliveTextbox.SetActive(true);
                break;    
            case CharacterNames.Siggie:                
                SiggieTextbox.SetActive(true);
                break;    
            case CharacterNames.Interactable:                
                InteractableTextbox.SetActive(true);
                break;    
        }

        switch (arrows)
        {
            case ArrowsToShow.Forward:
                ArrowForward.SetActive(true);
                break;
            case ArrowsToShow.Backward:
                ArrowBackward.SetActive(true);
                break;
            case ArrowsToShow.Both:
                ArrowForward.SetActive(true);
                ArrowBackward.SetActive(true);
                break;    
        }
    }

    public void HideText()
    {
        HumphreyTextbox.SetActive(false);
        GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().StopTalk();
        AnneTextbox.SetActive(false);
        DottieTextbox.SetActive(false);
        MadsTextbox.SetActive(false);
        MadMadsTextbox.SetActive(false);
        OliveTextbox.SetActive(false);
        SiggieTextbox.SetActive(false);
        InteractableTextbox.SetActive(false);

        ArrowForward.SetActive(false);
        ArrowBackward.SetActive(false);
    }

    public void ShowArrowForward()
    {
        ArrowForward.SetActive(true);
    }

    public void ShowArrowBackward()
    {
        ArrowBackward.SetActive(true);
    }

    public void OnBackButtonDown()
    {
        if(dialogue != null)
        {
            dialogue.DialogueBackward();
            GetComponent<AudioSource>().Play();
        }

    }

    public void OnForwardButtonDown()
    {
        if (dialogue != null)
        {
            dialogue.DialogueForward();
            GetComponent<AudioSource>().Play();
        }

    }

    public void SetDialogue(DialogueNodes.Dialogue _dialogue)
    {
        dialogue = _dialogue;
    }
}
