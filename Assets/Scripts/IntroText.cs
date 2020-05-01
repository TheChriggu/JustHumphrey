using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] public string SceneToLoad = "";
    [SerializeField] GameObject introText;
    [SerializeField] GameObject startScene;
    [SerializeField] GameObject startSceneGrab;

    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                counter++;

                switch (counter)
                {
                    case 1:
                        //introText.SetActive(false);
                        startScene.SetActive(true);
                        break;
                    case 2:
                        startSceneGrab.SetActive(true);
                        break;
                    default:
                        PlayerPrefs.SetString("CurrentScene", SceneToLoad);
                        GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>().TransitionToRoom(SceneToLoad, gameObject.scene, new Vector3(0.0f, -4.328f, 0.0f));
                        break;
                }
            }
        }
#endif
#if !UNITY_ANDROID
        if (Input.GetMouseButtonDown(0))
        {
            counter++;

            switch (counter)
            {
                case 1:
                    startScene.SetActive(true);
                    break;
                case 2:
                    startSceneGrab.SetActive(true);
                    break;
                default:
                    PlayerPrefs.SetString("CurrentScene", SceneToLoad);
                    GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>().TransitionToRoom(SceneToLoad, gameObject.scene, new Vector3(0.0f, -4.328f, 0.0f));
                    break;
            }
        }
#endif
    }
}
