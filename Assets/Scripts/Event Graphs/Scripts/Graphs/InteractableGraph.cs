using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using GoogleSheetsToUnity;


[CreateAssetMenu]
public class InteractableGraph : NodeGraph 
{ 
	public Interactable.UtilityNodes.StartInteractable EventStarter;
    public EventNode CurrentlyActiveEvent;
    public GameObject Interactable; //The interactbale this graph is attached to



    //all dialogues viewable at:
    //https://docs.google.com/spreadsheets/d/1Zb4YGXjNC6Kinf9wRTLlXCsIU14vRr_jmabGfM2XrVo/edit?usp=sharing
    //Functionality for Google Sheets To Unity has been removed for code publication
    //nothing can be im- or exported
    string associatedSheet = ""; //Part of google spreadsheet URL. See Google Sheets To Unity Documentation
    GstuSpreadSheet spreadSheet;

    //Start Object Interaction
    public virtual void StartEvent(GameObject _interactable)
    {
        Interactable = _interactable;

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is Interactable.UtilityNodes.StartInteractable)
            {
                Interactable.UtilityNodes.StartInteractable node = nodes[i] as Interactable.UtilityNodes.StartInteractable;
                EventStarter = node;
                CurrentlyActiveEvent = EventStarter;
            }
        }
        EventStarter.ReInitialize();
    }

    public virtual void OnHitByRaycast(string raycastSource)
    {
         for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is Interactable.UtilityNodes.StartInteractable)
            {
                Interactable.UtilityNodes.StartInteractable node = nodes[i] as Interactable.UtilityNodes.StartInteractable;
                node.StartRaycastEvent(raycastSource);
            }
        }
    }

    public void GameObjectClicked()
    {
        if (CurrentlyActiveEvent is Interactable.UtilityNodes.ClickOnGameObject)
        {
            Interactable.UtilityNodes.ClickOnGameObject node = CurrentlyActiveEvent as Interactable.UtilityNodes.ClickOnGameObject;
            node.Clicked();
        }
    }

    public void StartInteraction(string interactionName)
    {
        switch (interactionName)
        {
            case "Talk":
                EventStarter.ClickedTalk();
                break;
            case "Take":
                EventStarter.ClickedTake();
                break;
            case "LookAt":
                EventStarter.ClickedLookAt();
                break;
            case "Gnaw":
                EventStarter.ClickedGnaw();
                break;
        }
    }

    public int GetPossibleInteractions()
    {
        return EventStarter.GetPossibleInteractions();
    }

    //End Object Interaction
    //Start Dialogue Im- and Export
    public virtual void GenerateIDs()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is DialogueNodes.Dialogue)
            {
                DialogueNodes.Dialogue node = nodes[i] as DialogueNodes.Dialogue;
                node.GenerateID();
            }
        }
    }

    public virtual void ExportIDs()
    {
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, GetCategory()), exportIDsInternal);
        
    }

    public virtual string GetCategory()
    {
        SetStarter();
        return EventStarter.GetCategory();
    }

    bool checkIfIDExists(string ID)
    {
        var column = spreadSheet.columns["A"];

        foreach(GSTU_Cell cell in column)
        {
            if (cell.value == ID)
            {
                return true;
            }
        }

        return false;
    }

    void exportIDsInternal(GstuSpreadSheet ss)
    {
        spreadSheet = ss;
        List<List< string >> AllIDs = new List<List<string>>();

        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is DialogueNodes.Dialogue)
            {
                DialogueNodes.Dialogue node = nodes[i] as DialogueNodes.Dialogue;
                string id = node.GetID();

                if (!checkIfIDExists(id))
                {
                    List<string> diaID = new List<string>(){id};
                    List<string> lineID = new List<string>() { generateLineID(id)};
                    AllIDs.Add(diaID);
                    AllIDs.Add(lineID);

                    node.LockID();
                }
            }
        }

        SpreadsheetManager.Append(new GSTU_Search(associatedSheet, GetCategory(), "A2"), new ValueRange(AllIDs), null);
    }

    string generateLineID(string diaID)
    {
        string lineID = diaID;

        lineID = lineID.Replace("DiaID", "LineID");
        lineID += "_00";

        Debug.Log("Generated Line ID: " + lineID);

        return lineID;
    }

    public void ImportDialogue()
    {
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, GetCategory(), "A1", "C1000"), importDialogueInternal);
    }

    void importDialogueInternal(GstuSpreadSheet ss)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is DialogueNodes.Dialogue)
            {
                DialogueNodes.Dialogue node = nodes[i] as DialogueNodes.Dialogue;
                node.LoadDialogue(ss);
            }
        }
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    //End Dialogue Im- and Export

    //Set Starter Node
    public virtual void SetStarter()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is Interactable.UtilityNodes.StartInteractable)
            {
                Interactable.UtilityNodes.StartInteractable node = nodes[i] as Interactable.UtilityNodes.StartInteractable;
                EventStarter = node;
                CurrentlyActiveEvent = EventStarter;
            }
        }
    }

}