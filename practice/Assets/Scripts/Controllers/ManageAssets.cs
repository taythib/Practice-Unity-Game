 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ManageAssets : MonoBehaviour {
    // Sets assets that are available for use
    public static List<string> availableAssets;
    public static List<AssetData> items;

    // Use this for initialization
    void Awake () {

        TextAsset json = Resources.Load("ItemDB.json") as TextAsset;
        Debug.Log(json.text);
        var wrappedJson = JsonUtility.FromJson<MyWrapper>(json.text);
        items = wrappedJson.items;
        //AssetData ob = new AssetData();
        //ob = ob.CreateFromJSON(json.text);
        //Debug.Log(ob.name);
        availableAssets = new List<string>
        {
            items[0].name,
            items[1].name,
            items[2].name
        };
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}
}

[System.Serializable]
public class MyWrapper
{
    public List<AssetData> items;
}

[System.Serializable]
public class AssetData
{
    public int id;
    public string name;
    public int sheetIndex;
    public string spriteSheet;
    public string type;
    public Properties properties;

    // Use this for initialization
    public AssetData()
    {
    }

    public AssetData(int id, string name, int sheetIndex, string spriteSheet, string type, Properties properties)
    {
        this.id = id;
        this.name = name;
        this.sheetIndex = sheetIndex;
        this.spriteSheet = spriteSheet;
        this.type = type;
        this.properties = properties;
    }


    // Update is called once per frame
    public void Update()
    {
    }

    [System.Serializable]
    public class Properties
    {
        public string hunger;
    }
}