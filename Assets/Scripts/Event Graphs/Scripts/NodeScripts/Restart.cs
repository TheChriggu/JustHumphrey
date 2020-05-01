using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UtilityNodes
{
    public class Restart : EventNode
    {
        [Input] public Empty PreviousNode;

        protected override void Init()
        {
            base.Init();
        }


        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return this;
        }

        public override void StartEvent()
        {
            if (graph is InteractableGraph)
            {
                //set node as currently active node
                InteractableGraph ownGraph = graph as InteractableGraph;
                ownGraph.CurrentlyActiveEvent = ownGraph.EventStarter;
            }

            else
            {
                InventoryItemGraph ownGraph = graph as InventoryItemGraph;
                ownGraph.CurrentlyActiveEvent = ownGraph.InventoryStarter;
            }

            return;
        }

        public override Color GetColor()
        {
            if (GetInputPort("PreviousNode").ConnectionCount == 0)
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