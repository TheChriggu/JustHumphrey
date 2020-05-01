using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNodeEditor;

[CustomNodeGraphEditor(typeof(InventoryItemGraph))]
public class InventoryItemGraphEditor : NodeGraphEditor
{ 
    //Limit available nodes to those usable in Inventory Item Graphs
	public override string GetNodeMenuName(System.Type type) {
		if (type.Namespace == "XNode.Examples.MathNodes" 
        || type.Namespace == "XNode.Examples.StateGraph" 
        || type.Name == "EventNode" 
        || type.Namespace.Contains("Interactable.")
        || type.Namespace.Contains("Deprecated"))
        {
			return null;
		} 
        else 
        {
            string str = base.GetNodeMenuName(type);
            if (str.Contains("Inventory/"))
            {
                str = str.Replace("Inventory/", "");
            }
            return str;
        }
	}
}