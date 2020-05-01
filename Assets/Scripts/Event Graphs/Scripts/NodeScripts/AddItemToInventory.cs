using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace InventoryNodes
{
    public class AddItemToInventory : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;

        public InventoryItemGraph InventoryItemGraph;


        // Use this for initialization
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
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = this;

            //add sprite and graph to inventory
            InventoryController inventory = GameObject.Find("Inventory").GetComponent<InventoryController>();
            inventory.AddItemToInventory(InventoryItemGraph);

            //activate next node
            NodePort exitPort = GetOutputPort("NextNode");
            EventNode node = exitPort.Connection.node as EventNode;
            node.StartEvent();

            return;
        }

        public override Color GetColor()
        {
            if (InventoryItemGraph == null)
            {
                return Color.red;
            }

            if (GetOutputPort("NextNode").ConnectionCount != 1)
            {
                return Color.red;
            }

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