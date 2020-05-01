using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;
using XNode;



[CustomNodeEditor(typeof(UtilityNodes.Controller))]
public class ControllerEditor : EventNodeEditor
{
    public override Color GetTint()
    {
        return Color.green;
    }

    public override void OnBodyGUI()
    {
        base.OnBodyGUI();
        EventNode node = target as EventNode;
        InteractableGraph graph = node.graph as InteractableGraph;
        if (GUILayout.Button("Generate Dialogue IDs")) graph.GenerateIDs();
        if (GUILayout.Button("Export Dialogue IDs")) graph.ExportIDs();
        if (GUILayout.Button("Import Dialogues")) graph.ImportDialogue();
    }
}