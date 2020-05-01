using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDialogueHandler : MonoBehaviour
{
    public void GenerateIDs()
    {
        Object[] allGraphs = Resources.LoadAll("Graphs");

        foreach(var graph in allGraphs)
        {
            if (graph is InteractableGraph || graph is InventoryItemGraph)
            {
                (graph as InteractableGraph).GenerateIDs();
            }

        }
    }

    public void ExportIDs()
    {
        Object[] allGraphs = Resources.LoadAll("Graphs");

        foreach (var graph in allGraphs)
        {
            if (graph is InteractableGraph || graph is InventoryItemGraph)
            {
                (graph as InteractableGraph).ExportIDs(); ;
            }
        }
    }
    public void LoadAllDialogues()
    {
        Object[] allGraphs = Resources.LoadAll("Graphs");

        foreach (Object graph in allGraphs)
        {
            if (graph is InventoryItemGraph)
            {
                //(graph as InteractableGraph).SetDirty();
                (graph as InventoryItemGraph).ImportDialogue();
            }

           else if (graph is InteractableGraph)
            {
                //(graph as InteractableGraph).SetDirty();
                (graph as InteractableGraph).ImportDialogue();
            }
        }
    }
}
