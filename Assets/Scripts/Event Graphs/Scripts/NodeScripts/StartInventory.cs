using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Inventory.UtilityNodes
{
    public class StartInventory : EventNode
    {
        [Output] public Empty NextNode;
        [Output] public Empty LookAt;
        public string ItemName = "";
        public Sprite InventoryIcon;

        // Use this for initialization
        protected override void Init()
        {
            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void StartEvent()
        {
            //set node as currently active node
            InventoryItemGraph ownGraph = graph as InventoryItemGraph;
            ownGraph.CurrentlyActiveEvent = this;

            //set graph item name
            ownGraph.ItemName = ItemName;

            //activate the next node
            NodePort port = GetOutputPort("NextNode");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();

            return;
        }

        public void StartLookAt()
        {
            //set node as currently active node
            InventoryItemGraph ownGraph = graph as InventoryItemGraph;
            ownGraph.CurrentlyActiveEvent = this;

            //set graph item name
            ownGraph.ItemName = ItemName;

            //activate the next node
            NodePort port = GetOutputPort("LookAt");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();

            return;
        }

        public override Color GetColor()
        {
            if (InventoryIcon == null)
            {
                return Color.red;
            }

            if (GetOutputPort("NextNode").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (GetOutputPort("LookAt").ConnectionCount != 1)
            {
                return Color.red;
            }


            if (ItemName == "")
            {
                return Color.red;
            }

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }

        public Sprite GetInventoryIcon()
        {
            return InventoryIcon;
        }

        public string GetName()
        {
            return ItemName;
        }
    }
}