using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class InventoryItemGraph : InteractableGraph 
{ 
    public string ItemName;

    public Inventory.UtilityNodes.StartInventory InventoryStarter;

    public override void StartEvent(GameObject _interactable)
    {
        Interactable = _interactable;
        SetStarter();
        InventoryStarter.StartEvent();
    }

    public void LookAt()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is Inventory.UtilityNodes.StartInventory)
            {
                Inventory.UtilityNodes.StartInventory node = nodes[i] as Inventory.UtilityNodes.StartInventory;
                node.StartLookAt();
            }
        }        
    }

    public override string GetCategory()
    {
        return "ItemDescription";
    }

    public Sprite GetInventoryIcon()
    {
        return InventoryStarter.GetInventoryIcon();
    }

    public string GetName()
    {
        SetStarter();
        return InventoryStarter.GetName();
    }

    public override void SetStarter()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is Inventory.UtilityNodes.StartInventory)
            {
                Inventory.UtilityNodes.StartInventory node = nodes[i] as Inventory.UtilityNodes.StartInventory;
                InventoryStarter = node;               
            }
        }
    }
}