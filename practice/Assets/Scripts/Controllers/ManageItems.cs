using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is used to track ALL items present in the overworld. Anytime an item is picked up or used it must be destroyed, and when placed or received it must be added here
public class ManageItems : MonoBehaviour {
    public static List<GameObject> items;
    GameObject testFood;


    // Use this for initialization
    void Start () {
        items = new List<GameObject>();
        items.Add(new GameObject());
        AssetData testData = new AssetData();
        testData = ManageAssets.items[1];
        items[0] = Instantiate(Resources.Load("Food") as GameObject, new Vector3(14.5f, 1f, -5), Quaternion.identity);
        items[0].GetComponent<ItemDataManager>().SetData(items[0], testData);
    }
	
    // Use this for creating items in the overworld and adding them to controller array
    public static void createItem(float x, float y, AssetData itemData)
    {
        items.Add(new GameObject());
        items[items.Count - 1] = Instantiate(Resources.Load("Food") as GameObject, new Vector3(x, y, -5), Quaternion.identity);
        items[items.Count - 1].GetComponent<ItemDataManager>().SetData(items[items.Count - 1], itemData);
        Debug.Log(items.Count);
    }

    public static void removeItem(GameObject obj)
    {
        Debug.Log("removed!");
        items.Remove(obj);
        Destroy(obj);
    }
}
