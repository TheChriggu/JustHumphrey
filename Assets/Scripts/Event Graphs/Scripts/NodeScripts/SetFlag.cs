using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System;
using System.IO;


namespace GameFlagNodes
{
    public class SetFlag : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;

        public string FlagToSet;


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

            //add ItemState to ItemState set
            GameStateTracker.GameFlags.Add(FlagToSet);
            saveFlag();

            //activate next node
            NodePort exitPort = GetOutputPort("NextNode");
            EventNode node = exitPort.Connection.node as EventNode;
            node.StartEvent();

            return;
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

            if (FlagToSet == "")
            {
                return Color.red;
            }

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }

        private void saveFlag()
        {
            StreamWriter sw;
            string strg = Application.persistentDataPath + "\\" + "FlagData.txt";
            string path = @strg;

            if (!File.Exists(path))
            {
                // Create a file to write to.
                sw = File.CreateText(path);
            }

            using (sw = File.AppendText(path))
            {
                sw.WriteLine(FlagToSet);
            }

        }
    }
}