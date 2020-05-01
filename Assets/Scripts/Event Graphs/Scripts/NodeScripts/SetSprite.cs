using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UtilityNodes
{
    public class SetSprite : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;
        public Sprite Sprite;

        SpriteRenderer objectSprite;

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

            //set the sprite
            objectSprite = ownGraph.Interactable.GetComponent<SpriteRenderer>();
            objectSprite.sprite = Sprite;

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

            if (Sprite == null)
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