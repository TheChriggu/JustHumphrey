using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace UtilityNodes
{
public class WaitSeconds : EventNode
{
	[Input] public Empty PreviousNode;
    [Output] public Empty NextNode;
	public float SecondsToWait;


	// Use this for initialization
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return null; // Replace this
	}

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

	public IEnumerator StartEventCoroutine()
    {
        yield return new WaitForSeconds(SecondsToWait);

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

		if (SecondsToWait <= 0)
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