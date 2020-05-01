using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Interactable.UtilityNodes
{
    public class TurnOnGameObject : EventNode
    {

        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;
        SpriteRenderer interactableRenderer;
        BoxCollider2D interactableBoxCollider;


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

            //set the interactable components
            interactableRenderer = ownGraph.Interactable.GetComponent<SpriteRenderer>();
            interactableBoxCollider = ownGraph.Interactable.GetComponent<BoxCollider2D>();

            //deactivate gameObjects renderer & collider
            interactableRenderer.enabled = true;
            interactableBoxCollider.enabled = true;

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

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }
    }
}