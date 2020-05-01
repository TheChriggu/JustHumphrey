using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public GameObject Iris;
    public InventorySlotController InventorySlots;
    public StartGame UICamera;

    private float irisOpenSize = 70;
    private float speed = 1.5f;
    private Vector3 irisOffset = new Vector3(0.0f, 4f, 0.0f);

    private Vector3 startPosition = new Vector3(0.0f, -4.328f, 0.0f);
    private string currentScene;
    
    public void StartNewGame()
    {
        InventorySlots.ClearInventroy();
        UICamera.ResetGame();

        SceneManager.LoadScene("StartScreen");
    }

    public void FinishGame()
    {
        InventorySlots.ClearInventroy();
        UICamera.ResetGame();

        SceneManager.LoadScene("Credits");
    }

    public void TransitionToRoom(string sceneToLoad, Scene sceneToUnload, Vector3 positionInNewRoom)
    {
        currentScene = sceneToLoad;
        StartCoroutine(Transition(sceneToLoad, sceneToUnload, positionInNewRoom));
    }

    IEnumerator Transition(string sceneToLoad, Scene sceneToUnload, Vector3 positionInNewRoom)
    {
        Iris.transform.position = GameObject.Find("Hamster").transform.position + irisOffset;
        for (float size = irisOpenSize; size > 0; size -= speed)
        {
            Vector3 newSize = Iris.transform.localScale;
            newSize.x = size;
            newSize.y = size;
            Iris.transform.localScale = newSize;
            yield return new WaitForSeconds(.001f);
        }

        SceneManager.UnloadSceneAsync(sceneToUnload);
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);

        startPosition = positionInNewRoom;
        Iris.transform.position = positionInNewRoom + irisOffset;

        for (float size = 0; size < irisOpenSize; size += speed)
        {
            Vector3 newSize = Iris.transform.localScale;
            newSize.x = size;
            newSize.y = size;
            Iris.transform.localScale = newSize;
            yield return new WaitForSeconds(.001f);
        }
        yield return null;
    }

    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    public bool isScene_CurrentlyLoaded(string sceneName_no_extention)
    {
        if (currentScene != null)
        {
            return sceneName_no_extention == currentScene;
        }
        else
        {


            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);

                if (scene.name == sceneName_no_extention)
                {

                    //the scene is already loaded
                    return true;
                }
            }
            return false;
        }

    }
}
