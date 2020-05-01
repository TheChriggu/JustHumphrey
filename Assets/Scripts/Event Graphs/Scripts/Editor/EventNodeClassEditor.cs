using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
using XNode;



[CustomNodeEditor(typeof(EventNode))]
public class EventNodeEditor : NodeEditor 
{
	public override Color GetTint()
	{
		EventNode node = target as EventNode;
		InteractableGraph graph = node.graph as InteractableGraph;

		Color nodeColor = node.GetColor();
			return nodeColor;		
	}
}

