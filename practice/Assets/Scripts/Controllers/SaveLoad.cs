using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {
    //it's static so we can call it from anywhere
    public static void Save()
    {
        //string json = JsonUtility.ToJson(SlimeDataManager.slimeData);
        //File.WriteAllText(Application.persistentDataPath + "/slime.json", json.ToString());
        //Debug.Log("Saved!");
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/slime.json"))
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + "/slime.json");
            string json = reader.ReadToEnd();
            Debug.Log(json);
            ManageSlime.slimeData = ManageSlime.slimeData.CreateFromJSON(json);
            reader.Close();
            Debug.Log("Loaded from path: "+ Application.persistentDataPath);
        }
    }
}