using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEngine.SceneManagement;

namespace Interactable.RoomFunctions
{
    public class LoadScene : EventNode
    {
        [Input] public Empty PreviousNode;

		public string SceneToLoad = "";

        // Use this for initialization
        protected override void Init()
        {
            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return this;
        }

        public override void StartEvent()
        {
            //set node as currently active node
            InteractableGraph ownGraph = graph as InteractableGraph;
            ownGraph.CurrentlyActiveEvent = this;

            PlayerPrefs.SetString("CurrentScene", SceneToLoad);
            GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>().TransitionToRoom(SceneToLoad,(graph as InteractableGraph).Interactable.scene, new Vector3(0.0f,-4.328f,0.0f));

            //finished
            return;
        }

        public override Color GetColor()
        {
            if (GetInputPort("PreviousNode").ConnectionCount == 0)
            {
                return Color.red;
            }

			if (SceneToLoad == "")
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