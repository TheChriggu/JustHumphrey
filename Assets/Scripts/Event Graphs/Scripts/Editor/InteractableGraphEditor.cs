using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(InteractableGraph))]
public class InteractableGraphEditor : NodeGraphEditor
{
    //Limit available nodes to those usable in EventGraphs
    public override string GetNodeMenuName(System.Type type)
    {
        if (type.Namespace == "XNode.Examples.MathNodes"
        || type.Namespace == "XNode.Examples.StateGraph"
        || type.Name == "EventNode"
        || type.Namespace.Contains("Inventory.")
        || type.Name.Contains("Deprecated"))
        {
            return null;
        }

        else
        {
            string str = base.GetNodeMenuName(type);
            if (str.Contains("Interactable/"))
            {
                str = str.Replace("Interactable/", "");
            }
            return str;
        }
    }
}