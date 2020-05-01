using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using GoogleSheetsToUnity;

    //all dialogues viewable at:
    //https://docs.google.com/spreadsheets/d/1Zb4YGXjNC6Kinf9wRTLlXCsIU14vRr_jmabGfM2XrVo/edit?usp=sharing
    //Functionality for Google Sheets To Unity has been removed for code publication
    //nothing can be im- or exported

namespace DialogueNodes
{
    public class Dialogue : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;


        public string DialogueID;
        public bool IDLock = false;

        public string[] texts;
        public CharacterNames[] names;
        int currentlyShownText = 0;


        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = this;

            //set text on dialogue window
            DialogueInterface Dialogue = GameObject.Find("Dialogue").GetComponent<DialogueInterface>();
            Dialogue.SetText(texts[currentlyShownText], names[currentlyShownText]);
            Dialogue.ShowText(names[currentlyShownText], DialogueInterface.ArrowsToShow.Forward);
            Dialogue.SetDialogue(this);            
        }


        public void DialogueForward()
        {
            currentlyShownText++;
            if(currentlyShownText < texts.Length)
            {
                //set text on dialogue window
                DialogueInterface Dialogue = GameObject.Find("Dialogue").GetComponent<DialogueInterface>();
                Dialogue.SetText(texts[currentlyShownText], names[currentlyShownText]);
                Dialogue.ShowText(names[currentlyShownText], DialogueInterface.ArrowsToShow.Both);
            }

            else
            {
                currentlyShownText = 0;
                GameObject.Find("Dialogue").GetComponent<DialogueInterface>().SetDialogue(null); 
                //activate the next node
                NodePort port = GetOutputPort("NextNode");
                EventNode node = port.Connection.node as EventNode;
                node.StartEvent();
            }
        }

        public void DialogueBackward()
        {
            currentlyShownText--;
            if(currentlyShownText > 0)
            {
                //set text on dialogue window
                DialogueInterface Dialogue = GameObject.Find("Dialogue").GetComponent<DialogueInterface>();
                Dialogue.SetText(texts[currentlyShownText], names[currentlyShownText]);
                Dialogue.ShowText(names[currentlyShownText], DialogueInterface.ArrowsToShow.Both);
            }

            else
            {
                //set text on dialogue window
                DialogueInterface Dialogue = GameObject.Find("Dialogue").GetComponent<DialogueInterface>();
                Dialogue.SetText(texts[currentlyShownText], names[currentlyShownText]);
                Dialogue.ShowText(names[currentlyShownText], DialogueInterface.ArrowsToShow.Forward);
            }
        }

        bool GetIsClicked()
        {
            if (Input.touchCount > 0)
            {
                return true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                return true;
            }
            return false;
        }

        public override Color GetColor()
        {
            if (GetInputPort("PreviousNode").ConnectionCount == 0)
            {
                return Color.red;
            }

            if (GetOutputPort("NextNode").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (DialogueID.Length == 0)
            {
                return new Color(0.8f, 0, 0, 1);
            }

            if (!IDLock)
            {
                return new Color(0.8f, 0.2f, 0, 1);
            }

            if (texts.Length == 0)
            {
                return new Color(0.8f, 0, 0, 1);
            }

            foreach (CharacterNames name in names)
            {
                if (name == CharacterNames.NotSelected)
                {
                    return new Color(0.8f, 0, 0, 1);
                }
            }

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }

        public void GenerateID()
        {
            if (IDLock)
            {
                return;
            }

            bool isPortFound = false;
            EventNode currentNode = this;

            DialogueID = "DiaID_" + graph.name + "_";
            int counter = 0;
            string path = "";

            while (!isPortFound)
            {
                EventNode connectedNode = currentNode.GetInputPort("PreviousNode").GetConnection(0).node as EventNode;

                if (connectedNode is Dialogue)
                {
                    counter += 1;
                }

                if (connectedNode is LogicNodes.IfElseGate)
                {
                    if (currentNode.GetInputPort("PreviousNode").GetConnection(0).fieldName == "True")
                    {
                        path = path.Insert(0, "O");
                    }
                    else
                    {
                        path = path.Insert(0, "X");
                    }
                }

                if (connectedNode is LogicNodes.NamedNode)
                {
                    path = (connectedNode as LogicNodes.NamedNode).GetName() + "_" + path;
                }

                if (connectedNode.HasPort("PreviousNode"))
                {
                    currentNode = connectedNode;
                }
                else
                {
                    isPortFound = true;
                    string branchName = currentNode.GetInputPort("PreviousNode").GetConnection(0).fieldName;
                    if (branchName == "ReactToInventoryItem")
                    {
                        branchName = "ReactTo";
                    }
                    DialogueID += branchName;
                }
            }

            if (path != "")
            {
                DialogueID += "_" + path;
            }

            if (!DialogueID.EndsWith("_"))
            {
                DialogueID += "_";
            }

            if (counter < 10)
            {
                DialogueID += "0";
            }
            DialogueID += counter.ToString();

            DialogueID = DialogueID.Replace(" ", "");
        }

        public string GetID()
        {
            return DialogueID;
        }

        int getDialogueLength(GstuSpreadSheet spreadSheet)
        {
            int length = 0;
            bool isCounting = false;

            var column = spreadSheet.columns["A"];

            foreach (GSTU_Cell cell in column)
            {
                if (cell.value.Contains("DiaID_"))
                {
                    isCounting = false;
                }

                if (cell.value == "")
                {
                    isCounting = false;
                }

                if (isCounting)
                {
                    length += 1;
                }

                if (cell.value == DialogueID)
                {
                    isCounting = true;
                }
            }

            return length;
        }

        void setDialogue(GstuSpreadSheet spreadSheet)
        {
            bool isWriting = false;
            int dialogueCounter = 0;

            var textColumn = spreadSheet.columns["A"];

            int i = 0;
            foreach (GSTU_Cell cell in textColumn)
            {
                if (cell.value.Contains("DiaID_"))
                {
                    isWriting = false;
                }

                if (cell.value == "")
                {
                    isWriting = false;
                }

                if (isWriting)
                {

                    int row = cell.Row();

                    string nameCell = "B" + row.ToString();
                    string textCell = "C" + row.ToString();
                    Debug.Log("Loading names");
                    names[dialogueCounter] = CharacterNameFunctions.FromString(spreadSheet[nameCell].value);
                    texts[dialogueCounter] = spreadSheet[textCell].value;
                    dialogueCounter++;
                }

                if (cell.value == DialogueID)
                {
                    isWriting = true;
                }

                i++;
            }
        }


        public void LoadDialogue(GstuSpreadSheet spreadSheet)
        {
            int length = getDialogueLength(spreadSheet);
            texts = new string[length];
            names = new CharacterNames[length];

            setDialogue(spreadSheet);

            Debug.Log("Dialogue " + DialogueID + " loaded.");
        }

        public void LockID()
        {
            IDLock = true;
        }
    }
}