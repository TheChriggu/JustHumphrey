using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryController : MonoBehaviour
{
    public InventorySlotController ItemSlots;
    string mode;
    public bool isInventoryOpen = false;
    public OptionsMenuController Options;
    public void OpenInventory(string _mode)
    {
        //Tutorial, opens only first time inventory is used
        if(!GameStateTracker.GameFlags.Contains("InventoryOpened"))
        {
            GameStateTracker.GameFlags.Add("InventoryOpened");

            StreamWriter sw;
            string strg = Application.persistentDataPath + "\\" + "FlagData.txt";
            string path = @strg;

            if (!File.Exists(path))
            {
                // Create a file to write to.
                sw = File.CreateText(path);
            }

            using (sw = File.AppendText(path))
            {
                sw.WriteLine("InventoryOpened");
            }

            GameObject.Find("InventoryText").GetComponent<EventHandler>().OnHitByRaycast("Inventory");
        }
        //Tutorial end

        GameObject.Find("Hamster").GetComponent<HamsterController>().DisableHamsterMovement();
        mode = _mode;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        isInventoryOpen = true;
    }

    public void CloseInventory()
    {
        if(!Options.GetIsOpen())
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            GameObject.Find("Hamster").GetComponent<HamsterController>().EnableHamsterMovement();

            isInventoryOpen = false;
        }

    }

    public bool IsInventoryOpen()
    {
        return isInventoryOpen;
    }

    public void OnSlotClick(GameObject selectedObject)
    {
        if(mode == "Use")
        {
            InventoryItemSlot selectedSlot = selectedObject.GetComponent<InventoryItemSlot>();
            if(selectedSlot.IsSlotOccupied())
            {
                GameObject.Find("Hamster").GetComponent<HamsterController>().SetItemAsSelected(selectedSlot);
        
                CloseInventory();
            }

        }

        if(mode == "LookAt")
        {
            InventoryItemSlot selectedSlot = selectedObject.GetComponent<InventoryItemSlot>();
            selectedSlot.GetComponent<InventoryItemSlot>().graph.LookAt();

            mode = "Use";
        }

    }

    public void AddItemToInventory(InventoryItemGraph _graph)
    {
        transform.Find("ItemSlots").GetComponent<InventorySlotController>().AddItemToInventory(_graph);
    }

    public void RemoveCurrentItemFromInventory()
    {
        transform.Find("ItemSlots").GetComponent<InventorySlotController>().RemoveCurrentItemFromInventroy();
    }

    public void LookAt()
    {
        mode = "LookAt";
    }

    public void InitializeInventory()
    {
        //Generate Slot IDs
        ItemSlots.GenerateSlotIDs();
        ItemSlots.LoadAllSlots();
    }
}
