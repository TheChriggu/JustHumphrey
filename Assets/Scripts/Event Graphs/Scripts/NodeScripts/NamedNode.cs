using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace LogicNodes
{
    public class NamedNode : EventNode
    {

        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;


        public string Name;


        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void StartEvent()
        {
            NodePort truePort = GetOutputPort("NextNode");
            EventNode trueNode = truePort.Connection.node as EventNode;
            trueNode.StartEvent();
        }

        public string GetName()
        {
            return Name;
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

            if (Name == "")
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