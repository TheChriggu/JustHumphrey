﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.SceneManagement;

namespace Interactable.RoomFunctions
{
    public class RestartGame : EventNode
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

            GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>().FinishGame();

            //finished
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