using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UtilityNodes
{
    public class PlayAnimation : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;
        public enum Animations { Touch, Bite, Take, Open, Use }
        public Animations animation;

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

			switch (animation)
			{
				case Animations.Touch:
					GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().Touch();
					break;		
				case Animations.Take:
					GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().Take();
					break;	
				case Animations.Bite:
					GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().Bite();
					break;	
				case Animations.Open:
					GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().Open();
					break;		
				case Animations.Use:
					GameObject.Find("Hamster").GetComponent<HamsterAnimationController>().Use();
					break;			
			}


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