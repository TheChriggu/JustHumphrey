using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{
    public string SceneToLoad;
    public void OnButtonDown() 
    {
        //GameStateTracker.IsNewGame = isNewGame;
        SceneManager.LoadScene(SceneToLoad);
    }
}
