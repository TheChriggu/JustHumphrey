using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : MonoBehaviour
{
    public InventoryItemGraph graph;
    int slotID = 0;

    public void LoadGraph()
    {
        Object[] allGraphs = Resources.LoadAll("Graphs");

        string key = "Slot" + slotID;
        string name = PlayerPrefs.GetString(key);

        foreach(var graph in allGraphs)
        {
            if (graph is InventoryItemGraph)
            {
                string graphName = (graph as InventoryItemGraph).GetName();
                if(name == graphName)
                {
                    SetItem(graph as InventoryItemGraph);
                }
            }

        }
    }
    public void SetItem(InventoryItemGraph _graph)
    {
        graph = _graph;
        transform.GetChild(0).GetComponent<Image>().sprite = _graph.GetInventoryIcon();
        transform.GetChild(0).GetComponent<Image>().color = Color.white;


        string id = "Slot" + slotID;
        PlayerPrefs.SetString(id, _graph.GetName());
    }

    public void ResetItem()
    {
        graph = null;
        transform.GetChild(0).GetComponent<Image>().sprite = null;
        transform.GetChild(0).GetComponent<Image>().color = Color.clear;

        string id = "Slot" + slotID;
        PlayerPrefs.SetString(id, "");
    }

    public bool IsSlotOccupied()
    {
        return transform.GetChild(0).GetComponent<Image>().sprite != null;
    }

    public Sprite GetItemSprite()
    {
        return transform.GetChild(0).GetComponent<Image>().sprite;
    }

    public InventoryItemGraph GetItemGraph()
    {
        return graph;
    }

    public string GetItemName()
    {
        return "";
    }

    public void StartEvent()
    {
        graph.StartEvent(null);
    }

    public void SetSlotID(int id)
    {
        slotID = id;
    }
}
