using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Interactable.RoomFunctions
{
    public class TeleportHamsterToLocation : EventNode
    {

        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;
        [Input] public Vector3 Location = new Vector3(0, -4.05f, 0);

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = ownGraph.EventStarter;

            //Set Goal Location
            GameObject.Find("Hamster").GetComponent<HamsterController>().SetHamsterAtPosition(Location);

            //Start next node
            NodePort port = GetOutputPort("NextNode");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
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

			if (Location.x >= 7.5 || Location.x <= -7.5)
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