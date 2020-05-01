using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;


namespace LogicNodes
{
    public class IfElseGate : EventNode
    {

        [Input] public Empty PreviousNode;
        [Input] public bool Condition;
        [Output] public Empty True;
        [Output] public Empty False;
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
            NodePort port = GetInputPort("Condition");
            bool inputsAreTrue = true;
            object[] allInputs = port.GetInputValues();
            for (int i = 0; i < allInputs.Length; i++)
            {
                if (!((bool)allInputs[i]))
                {
                    inputsAreTrue = false;
                }
            }

            if (inputsAreTrue)
            {
                NodePort truePort = GetOutputPort("True");
                EventNode trueNode = truePort.Connection.node as EventNode;
                trueNode.StartEvent();
            }
            else
            {
                NodePort falsePort = GetOutputPort("False");
                EventNode falseNode = falsePort.Connection.node as EventNode;
                falseNode.StartEvent();
            }
        }

        public override Color GetColor()
        {
            if (GetInputPort("PreviousNode").ConnectionCount == 0)
            {
                return Color.red;
            }

            if (GetOutputPort("True").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (GetOutputPort("False").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (GetInputPort("Condition").ConnectionCount < 1)
            {
                return Color.yellow;
            }

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }
    }
}