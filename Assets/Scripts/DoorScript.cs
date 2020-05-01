using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public string SceneToLoad;
    public enum roomPositions{Left, Center, Right};
    public roomPositions positionInNewRoom;

    void OnMouseDown () 
    {
        HamsterController Hamster = GameObject.Find("Hamster").GetComponent<HamsterController>();
        if (Hamster.IsHamsterMovementEnabled())
        {
            Hamster.DisableHamsterMovement();
            Hamster.SetHamsterGoalPosition(Hamster.transform.position);

            PlayerPrefs.SetString("CurrentScene", SceneToLoad);

            SceneTransitionManager transition = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
            transition.TransitionToRoom(SceneToLoad, gameObject.scene, positionFromEnum(positionInNewRoom));
        }
        
    }

    private Vector3 positionFromEnum(roomPositions position)
    {
        switch (position)
        {
            case roomPositions.Left:
                return new Vector3(-7.0f,-4.328f,0.0f);

            case roomPositions.Center:
                return new Vector3(0.0f,-4.328f,0.0f);

            case roomPositions.Right:
                return new Vector3(7.0f,-4.328f,0.0f);                   
        }

        return new Vector3(0.0f,-4.328f,0.0f);
    }
}
