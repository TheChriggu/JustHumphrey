using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace Interactable.RoomFunctions
{
    public class FindInteractableByName : EventNode
    {

        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;
        [Output] public InteractableGraph Interactable;

        public string NameOfInteractable;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "Interactable")
            {
                return Interactable;
            }
            else
            {
                return null;
            }
        }

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = ownGraph.EventStarter;

            //Find the Interactable
            Interactable = GameObject.Find(NameOfInteractable).GetComponent<EventHandler>().graph;

            //Start next node
            NodePort port = GetOutputPort("NextNode");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
        }

        public override Color GetColor()
        {
            if (NameOfInteractable.Length == 0)
            {
                return Color.red;
            }

            if (GetInputPort("PreviousNode").ConnectionCount == 0)
            {
                return Color.red;
            }

            if (GetOutputPort("NextNode").ConnectionCount != 1)
            {
                return Color.red;
            }

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }
    }
}