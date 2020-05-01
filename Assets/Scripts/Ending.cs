using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private int counter = 0;
    [SerializeField] public string SceneToLoad = "";
    [SerializeField] GameObject humphreyFaints;
    [SerializeField] GameObject anneWasThePrettiest;
    [SerializeField] GameObject theBall;
    [SerializeField] GameObject siggieScene;

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
                        anneWasThePrettiest.SetActive(true);
                        break;
                    case 2:
                        theBall.SetActive(true);
                        break;
                    case 3:
                        humphreyFaints.SetActive(true);
                        break;
                    case 4:
                        siggieScene.SetActive(true);
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
                    anneWasThePrettiest.SetActive(true);
                    break;
                case 2:
                    theBall.SetActive(true);
                    break;
                case 3:
                    humphreyFaints.SetActive(true);
                    break;
                case 4:
                    siggieScene.SetActive(true);
                    break;
            }
        }
#endif
    }
}
