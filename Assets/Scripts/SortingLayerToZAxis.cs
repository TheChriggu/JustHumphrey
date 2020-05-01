using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerToZAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string layerName = "";
        Vector3 position = new Vector3(0,0,0);
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            layerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            position = transform.position;
        }
        
        if(gameObject.GetComponent<Canvas>() != null)
        {
            layerName = gameObject.GetComponent<Canvas>().sortingLayerName;
            position = transform.position;
        }

        switch (layerName)
        {
            case "Background":
                position.z = 90;
                break;
            case "Deko1":
                position.z = 80;
                break;
            case "Deko2":
                position.z = 70;
                break;
            case "Interactables":
                position.z = 60;
                break;
            case "Souvenirs":
                position.z = 50;
                break;
            case "Interactables2":
                position.z = 40;
                break;
            case "NPCs":
                position.z = 30;
                break;
            case "Humphrey":
                position.z = 20;
                break;
            case "UI":
                position.z = 10;
                break;
            case "Light":
                position.z = 0;
                break;
        }

        transform.position = position;
    }
}
