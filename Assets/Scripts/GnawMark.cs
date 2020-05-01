using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GnawMark : MonoBehaviour
{
    void Start()
    {
        string parentName = transform.parent.name;
        if (GameStateTracker.GameFlags.Contains(parentName + "Gnawed"))
        {
            ShowMark();
        }
    }
    public void ShowMark()
    {
        GetComponent<SpriteMask>().enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

        string parentName = transform.parent.name;
        GameStateTracker.GameFlags.Add(parentName + "Gnawed");


        StreamWriter sw;
        string strg = Application.persistentDataPath + "\\" + "FlagData.txt";
        string path = @strg;

        if (!File.Exists(path))
        {
            // Create a file to write to.
            sw = File.CreateText(path);
        }

        using (sw = File.AppendText(path))
        {
            sw.WriteLine(parentName + "Gnawed");
        }

    }
}
