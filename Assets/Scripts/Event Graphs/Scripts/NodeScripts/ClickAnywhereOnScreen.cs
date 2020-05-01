using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace UtilityNodes
{
    public class ClickAnywhereOnScreen : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty NextNode;
        [Output] public Vector2 CoordinatesClicked;


        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port) => CoordinatesClicked;

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = this;

            StartEventInternal();
        }


        public virtual void StartEventInternal()
        {
            GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(StartEventCoroutine());
        }

        bool GetIsClicked()
        {
            if (Input.touchCount > 0)
            {
                Camera cam = Camera.main;
                Touch touch = Input.GetTouch(0);
                CoordinatesClicked.x = cam.ScreenToWorldPoint(touch.position).x;
                CoordinatesClicked.y = cam.ScreenToWorldPoint(touch.position).y;
                return true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Camera cam = Camera.main;
                CoordinatesClicked = cam.ScreenToWorldPoint(Input.mousePosition);
                return true;
            }
            return false;
        }

        public IEnumerator StartEventCoroutine()
        {
            yield return new WaitWhile(() => GetIsClicked());
            yield return new WaitUntil(() => GetIsClicked());

            NodePort port = GetOutputPort("NextNode");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
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
