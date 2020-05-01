using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
public class EventHandler : MonoBehaviour
{
    [Header("Graph")]
	public InteractableGraph graph;

    // Start is called before the first frame update
    void Start()
    {
        graph.StartEvent(gameObject);
    }


    public void OnHitByRaycast(string raycastSource)
    {
        graph.OnHitByRaycast(raycastSource);
    }

    private void OnMouseDown()
    {
        GameObject.Find("Hamster").GetComponent<HamsterController>().SetHamsterGoalInteractable(gameObject);
    }

    public void StartInteraction()
    {
        graph.GameObjectClicked();
    }
}
