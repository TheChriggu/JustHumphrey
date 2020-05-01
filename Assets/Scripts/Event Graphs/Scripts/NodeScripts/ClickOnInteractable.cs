using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace RaycastNodes
{
    public class ClickOnInteractable : EventNode
    {
        [Input] public Empty PreviousNode;
        [Output] public Empty Nothing;
        [Output] public Empty ReactToInteractable;
        [Output] public Empty DefaultReaction;

        private Vector2 CoordinatesClicked;


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
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = this;

            //Get Click Coordinates
            GameObject.FindObjectOfType<MonoBehaviour>().StartCoroutine(GetClickCoroutine());
        }

        private void fireRaycast()
        {
            //start a raycast
            RaycastHit2D hit = Physics2D.Raycast(CoordinatesClicked, Vector2.zero, 20, LayerMask.GetMask("Interactable"));

            //start 'Nothing', if nothing was hit
            if (hit.collider == null)
            {
                NodePort exitPort = GetOutputPort("Nothing");
                EventNode node = exitPort.Connection.node as EventNode;
                node.StartEvent();
            }

            //check all special events, and start them if they are the correct one, else start the default node
            else
            {
                string ObjectName = hit.collider.name;
                GameObject ObjectHitByRaycast = hit.collider.gameObject;

                startInteractableReaction(ObjectHitByRaycast);


                NodePort port = GetOutputPort("ReactToInteractable");
                bool nextNodeStarted = false;

                if (port.IsConnected)
                {
                    for (int i = 0; i < port.ConnectionCount; i++)
                    {
                        if (port.GetConnection(i).node is LogicNodes.NamedNode)
                        {
                            LogicNodes.NamedNode node = port.GetConnection(i).node as LogicNodes.NamedNode;

                            if (node.GetName() == ObjectName)
                            {
                                node.StartEvent();
                                nextNodeStarted = true;
                            }
                        }
                    }
                }

                if (!nextNodeStarted)
                {
                    port = GetOutputPort("DefaultReaction");
                    EventNode node = port.Connection.node as EventNode;
                    node.StartEvent();
                }
            }
            return;
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

        public IEnumerator GetClickCoroutine()
        {
            yield return new WaitWhile(() => GetIsClicked());
            yield return new WaitUntil(() => GetIsClicked());

            fireRaycast();
        }

        private void startInteractableReaction(GameObject hitObject)
        {
            InventoryItemGraph ownGraph = graph as InventoryItemGraph;
            //Start the event in hit object
            hitObject.GetComponent<EventHandler>().OnHitByRaycast(ownGraph.ItemName);
        }

        public override Color GetColor()
        {
            if (GetInputPort("PreviousNode").ConnectionCount == 0)
            {
                return Color.red;
            }

            if (GetOutputPort("Nothing").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (GetOutputPort("DefaultReaction").ConnectionCount != 1)
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