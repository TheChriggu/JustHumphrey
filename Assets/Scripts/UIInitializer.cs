using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitializer : MonoBehaviour
{
    public GameObject Interactions;
    public GameObject Options;
    public GameObject Dialogue;
    public GameObject Inventory;
    public GameObject RestartDialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeInteractions();
        InitializeOptions();
        InitializeDialogues();
        InitializeInventory();
        InitializeRestartDialogue();
    }

    void InitializeInteractions()
    {
        foreach (Transform child in Interactions.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void InitializeOptions()
    {
        foreach (Transform child in Options.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void InitializeDialogues()
    {
        foreach (Transform child in Dialogue.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void InitializeInventory()
    {
        Inventory.GetComponent<InventoryController>().InitializeInventory();

        foreach (Transform child in Inventory.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void InitializeRestartDialogue()
    {
        foreach (Transform child in RestartDialogue.transform)
        {
            child.gameObject.SetActive(false);
        }        
    }
}
