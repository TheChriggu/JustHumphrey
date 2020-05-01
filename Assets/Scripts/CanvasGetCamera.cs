using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGetCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
