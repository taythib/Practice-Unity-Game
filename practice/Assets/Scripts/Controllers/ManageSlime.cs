using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSlime : MonoBehaviour {
    GameObject slimeLoad;
    GameObject mainSli;

    public static SlimeData slimeData;
    public static List<GameObject> slimes;

    // Use this for initialization
    void Start () {
        slimes = new List<GameObject>();
        slimes.Add(new GameObject());
        slimeData = new SlimeData(0, 0, 0, 0, "Slippy");
        SaveLoad.Load();
        slimes[0] = Instantiate(Resources.Load("Slime") as GameObject, new Vector3(2.5f, 0.5f, -5), Quaternion.identity);
        slimes[0].GetComponent<SlimeDataManager>().SetData(slimes[0], slimeData);
        createSlime(2.5f, 0.5f, slimeData);

    }

    // Use this for creating slimes in the overworld and adding them to controller array
    public static void createSlime(float x, float y, SlimeData slimeData)
    {
        slimes.Add(new GameObject());
        slimes[slimes.Count - 1] = Instantiate(Resources.Load("Slime") as GameObject, new Vector3(x, y, -5), Quaternion.identity);
        slimes[slimes.Count - 1].GetComponent<SlimeDataManager>().SetData(slimes[slimes.Count - 1], slimeData);
        Debug.Log(slimes.Count);
    }

    public static void removeSlime(GameObject obj)
    {
        Debug.Log("removed!");
        slimes.Remove(obj);
        Destroy(obj);
    }
}
