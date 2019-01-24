using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class ItemData{
    public int id;
    public string name;
    public int sheetIndex;
    public string spriteSheet;
    public string type;
    public Properties properties;

    // Use this for initialization
    public ItemData()
    {
    }

    public ItemData(int id, string name, int sheetIndex, string spriteSheet, string type, Properties properties) {
        this.id = id;
        this.name = name;
        this.sheetIndex = sheetIndex;
        this.spriteSheet = spriteSheet;
        this.type = type;
        this.properties = properties;
    }
	
	// Update is called once per frame
	public void Update () {
    }



    [System.Serializable]
    public class Properties
    {

    }
}
