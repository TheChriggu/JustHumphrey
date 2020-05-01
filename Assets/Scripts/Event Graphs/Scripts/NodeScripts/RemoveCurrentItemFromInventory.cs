﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Inventory.InventoryNodes
{
    public class RemoveCurrentItemFromInventory : EventNode
    {
        [Input] public Empty PreviousNode;

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

            GameObject.Find("Inventory").GetComponent<InventoryController>().RemoveCurrentItemFromInventory();
            GameObject.Find("Hamster").GetComponent<HamsterController>().DeselectCurrentItem();

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