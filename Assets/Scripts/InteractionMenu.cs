using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMenu : MonoBehaviour
{
    InteractableGraph graph;
    bool isOpen = false;

    public float Radius;
    public float MaxAngle;
    public GameObject TalkButton;
    public GameObject GnawButton;
    public GameObject LookAtButton;
    public GameObject TakeButton;
    public void StartInteraction(InteractableGraph _graph)
    {
        graph = _graph;

        int i = graph.GetPossibleInteractions();
        int numberOfButtons = enableButtons(i);

        Transform interactablePosition = graph.Interactable.transform.GetChild(0).transform;
        setButtonPosition(numberOfButtons, interactablePosition);


        if(!isOpen && i != 0)
        {
            GameObject.Find("Hamster").GetComponent<HamsterController>().DisableHamsterMovement();
            isOpen = true;
        }
    }

    int enableButtons(int i)
    {
        int enabledButtons = 0;
        if (i % 2 == 1)
        {
            GnawButton.SetActive(true);
            enabledButtons += 1;
        }

        i /= 2;
        if (i % 2 == 1)
        {
            LookAtButton.SetActive(true);
            enabledButtons += 1;
        }

        i /= 2;
        if (i % 2 == 1)
        {
            TalkButton.SetActive(true);
            enabledButtons += 1;
        }

        i /= 2;
        if (i % 2 == 1)
        {
            TakeButton.SetActive(true);
            enabledButtons += 1;
        }

        return enabledButtons;
    }

    void setButtonPosition(int numberOfButtons, Transform interactablePosition)
    {
        Vector3 position = transform.position;
        position.x = interactablePosition.position.x;
        position.y = interactablePosition.position.y;
        int currentButton = 0;
        float angle = MaxAngle / (float) (numberOfButtons+1);
        angle *= (Mathf.PI/180);

        //Positioning for all different buttons
        if (currentButton < numberOfButtons && TakeButton.activeSelf)
        {
            currentButton += 1;
            Vector3 offset = new Vector3 (Mathf.Cos(currentButton * angle), Mathf.Sin(currentButton * angle), 0);
            offset *= Radius;
            Vector3 buttonPosition = position + offset;

            float rotationDegrees = 180 / (numberOfButtons +1);
            rotationDegrees *= currentButton;
            rotationDegrees -= 90;
            Quaternion rotation = Quaternion.Euler(0, 0, rotationDegrees);

            TakeButton.transform.position = buttonPosition;
            TakeButton.transform.rotation = rotation;
        }

        if (currentButton < numberOfButtons && TalkButton.activeSelf)
        {
            currentButton += 1;
            Vector3 offset = new Vector3 (Mathf.Cos(currentButton * angle), Mathf.Sin(currentButton * angle), 0);
            offset *= Radius;
            Vector3 buttonPosition = position + offset;

            float rotationDegrees = 180 / (numberOfButtons +1);
            rotationDegrees *= currentButton;
            rotationDegrees -= 90;
            Quaternion rotation = Quaternion.Euler(0, 0, rotationDegrees);

            TalkButton.transform.position = buttonPosition;
            TalkButton.transform.rotation = rotation;
        }

        if (currentButton < numberOfButtons && LookAtButton.activeSelf)
        {
            currentButton += 1;
            Vector3 offset = new Vector3 (Mathf.Cos(currentButton * angle), Mathf.Sin(currentButton * angle), 0);
            offset *= Radius;
            Vector3 buttonPosition = position + offset;

            float rotationDegrees = 180 / (numberOfButtons +1);
            rotationDegrees *= currentButton;
            rotationDegrees -= 90;
            Quaternion rotation = Quaternion.Euler(0, 0, rotationDegrees);

            LookAtButton.transform.position = buttonPosition;
            LookAtButton.transform.rotation = rotation;
        }

        if (currentButton < numberOfButtons && GnawButton.activeSelf)
        {
            currentButton += 1;
            Vector3 offset = new Vector3 (Mathf.Cos(currentButton * angle), Mathf.Sin(currentButton * angle), 0);
            offset *= Radius;
            Vector3 buttonPosition = position + offset;

            float rotationDegrees = MaxAngle / (numberOfButtons +1);
            rotationDegrees *= currentButton;
            rotationDegrees -= MaxAngle/2;
            Quaternion rotation = Quaternion.Euler(0, 0, rotationDegrees);

            GnawButton.transform.position = buttonPosition;
            GnawButton.transform.rotation = rotation;
        }
    }

    void disableButtons()
    {
        TalkButton.SetActive(false);
        GnawButton.SetActive(false);
        LookAtButton.SetActive(false);
        TakeButton.SetActive(false);
    }

    public void TalkButtonClicked()
    {
        alertGraph("Talk");
    }

    public void TakeButtonClicked()
    {
        alertGraph("Take");
    }

    public void LookAtButtonClicked()
    {
        alertGraph("LookAt");
    }

    public void GnawButtonClicked()
    {
        alertGraph("Gnaw");
    }

    void alertGraph(string buttonName)
    {
        GetComponent<AudioSource>().Play();
        graph.StartInteraction(buttonName);
        disableButtons();
        GameObject.Find("Hamster").GetComponent<HamsterController>().EnableHamsterMovement();
        isOpen = false;
    }

    public bool GetIsMenuOpen()
    {
        return isOpen;
    }

    public void CloseInteractions()
    {
        disableButtons();
        GameObject.Find("Hamster").GetComponent<HamsterController>().EnableHamsterMovement();
        isOpen = false;        
    }
}
