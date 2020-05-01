using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Interactable.UtilityNodes
{
    public class StartInteractable : EventNode
    {
        [Output] public Empty Initialize;
        [Output] public Empty Take;
        [Output] public Empty Talk;
        [Output] public Empty LookAt;
        [Output] public Empty Gnaw;
        [Output] public Empty ReactToInventoryItem;
        [Output] public Empty DefaultReaction;

        public enum Categories { Unassigned, AnnesRoom, OlivesRoom, DottiesRoom, MadsRoom, TutorialRoom, ItemDescription, Ending, AnnesRoomAfterHole, OlivesRoomAfterHole, MadsRoomAfterHole, CreditsRoom, CreditsRoomAfterHole };
        public Categories Category;

        public override object GetValue(NodePort port)
        {
            return null;
        }
        public void ClickedTake()
        {
            //activate the next node
            NodePort port = GetOutputPort("Take");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
            return;
        }


        public void ClickedTalk()
        {
            //activate the next node
            NodePort port = GetOutputPort("Talk");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
            return;
        }


        public void ClickedLookAt()
        {
            //activate the next node
            NodePort port = GetOutputPort("LookAt");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
            return;
        }


        public void ClickedGnaw()
        {
            //activate the next node
            NodePort port = GetOutputPort("Gnaw");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
            return;
        }

        public void ReInitialize()
        {
            //activate the next node
            NodePort port = GetOutputPort("Initialize");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();
            return;
        }

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = this;

            //activate the next node
            NodePort port = GetOutputPort("NextNode");
            EventNode node = port.Connection.node as EventNode;
            node.StartEvent();

            return;
        }

        public void StartRaycastEvent(string raycastSource)
        {
            NodePort port = GetOutputPort("ReactToInventoryItem");
            bool nextNodeStarted = false;

            if (port.IsConnected)
            {
                for (int i = 0; i < port.ConnectionCount; i++)
                {
                    if (port.GetConnection(i).node is LogicNodes.NamedNode)
                    {
                        LogicNodes.NamedNode node = port.GetConnection(i).node as LogicNodes.NamedNode;

                        if (node.GetName() == raycastSource)
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

            return;
        }

        public override Color GetColor()
        {

            if (GetOutputPort("Initialize").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (GetOutputPort("DefaultReaction").ConnectionCount != 1)
            {
                return Color.red;
            }

            if (Category == Categories.Unassigned)
            {
                return Color.red;
            }

            if ((graph as InteractableGraph).CurrentlyActiveEvent == this)
            {
                return Color.blue;
            }

            return Color.white;
        }

        public int GetPossibleInteractions()
        {
            int result = 0;

            NodePort port = GetOutputPort("Take");
            if (port.IsConnected)
            {
                result += 1;
            }
            result *= 2;

            port = GetOutputPort("Talk");
            if (port.IsConnected)
            {
                result += 1;
            }
            result *= 2;

            port = GetOutputPort("LookAt");
            if (port.IsConnected)
            {
                result += 1;
            }
            result *= 2;

            port = GetOutputPort("Gnaw");
            if (port.IsConnected)
            {
                result += 1;
            }

            return result;
        }

        public string GetCategory()
        {
            switch (Category)
            {
                case Categories.Unassigned:
                    return "Unassigned";

                case Categories.TutorialRoom:
                    return "TutorialRoom";

                case Categories.MadsRoom:
                    return "MadsRoom";

                case Categories.ItemDescription:
                    return "ItemDescription";

                case Categories.AnnesRoom:
                    return "AnnesRoom";

                case Categories.OlivesRoom:
                    return "OlivesRoom";

                case Categories.DottiesRoom:
                    return "DottiesRoom";

                case Categories.Ending:
                    return "Ending";

                case Categories.AnnesRoomAfterHole:
                    return "AnnesRoomAfterHole";

                case Categories.MadsRoomAfterHole:
                    return "MadsRoomAfterHole";

                case Categories.OlivesRoomAfterHole:
                    return "OlivesRoomAfterHole";

                case Categories.CreditsRoom:
                    return "CreditsRoom";

                case Categories.CreditsRoomAfterHole:
                    return "CreditsRoomAfterHole";
            }

            return "Unassigned";
        }
    }
}