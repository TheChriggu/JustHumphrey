using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottiesRoomDoor : MonoBehaviour
{
    public InteractableGraph graph;

    void OnMouseUp()
    {
        if(GameObject.Find("Hamster").GetComponent<HamsterController>().IsHamsterMovementEnabled())
        {
            graph.StartEvent(gameObject);
        }

    }
}
