using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotController : MonoBehaviour
{
    public GameObject CurrentlySelectedObject;
    
    public void AddItemToInventory(InventoryItemGraph _graph)
    {
        bool isItemAssigned = false;
        foreach (Transform child in transform)
        {
            InventoryItemSlot slot = child.GetComponent<InventoryItemSlot>();
            if( !isItemAssigned && !slot.IsSlotOccupied())
            {
                slot.SetItem( _graph);
                isItemAssigned = true;
            }
        }
    }

    public void RemoveCurrentItemFromInventroy()
    {
        CurrentlySelectedObject.GetComponent<InventoryItemSlot>().ResetItem();
    }

    public void ClearInventroy()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<InventoryItemSlot>().ResetItem();
        }
 
    }

    public void OnSlotClick(GameObject selectedObject)
    {
        CurrentlySelectedObject = selectedObject;
    }

    public void GenerateSlotIDs()
    {
        int id = 0;
        foreach (Transform child in transform)
        {
            InventoryItemSlot slot = child.GetComponent<InventoryItemSlot>();
            slot.SetSlotID(id);
            id++;
        }
    }

    public void LoadAllSlots()
    {
        foreach (Transform child in transform)
        {
            InventoryItemSlot slot = child.GetComponent<InventoryItemSlot>();
            slot.LoadGraph();
        }
    }
}
