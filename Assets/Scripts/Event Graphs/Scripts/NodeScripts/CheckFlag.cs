using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace LogicNodes
{
    public class CheckFlag : EventNode
    {
        [Output] public bool True;
        [Output] public bool False;
        public string FlagToCheck;

        protected override void Init()
        {
            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == "True")
            {
                return GameStateTracker.GameFlags.Contains(FlagToCheck);
            }
            else
            {
                return !(GameStateTracker.GameFlags.Contains(FlagToCheck));
            }
        }

        public override Color GetColor()
        {
            if (FlagToCheck == "")
            {
                return Color.red;
            }

            if ((GetOutputPort("True").ConnectionCount == 0) && (GetOutputPort("False").ConnectionCount == 0))
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