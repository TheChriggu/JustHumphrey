using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Interactable.RoomFunctions
{
    public class ReInitializeOtherInteractable : EventNode
    {

        [Input] public Empty PreviousNode;
        [Input] public InteractableGraph Interactable;
        [Output] public Empty NextNode;

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = ownGraph.EventStarter;

            //Get input graph
            NodePort port = GetInputPort("Interactable");
            InteractableGraph i = port.GetInputValue() as InteractableGraph;
            i.EventStarter.ReInitialize();

            //start next node
            port = GetOutputPort("NextNode");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();

            return;

        }

        public override Color GetColor()
        {
            if (GetInputPort("PreviousNode").ConnectionCount == 0)
            {
                return Color.red;
            }

            if (GetInputPort("Interactable").ConnectionCount == 0)
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