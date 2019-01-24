using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataManager : MonoBehaviour {
    private GameObject instanceRef;
    public AssetData itemData;

    // Use this for initialization
    void Start () {

    }

    public void SetData(GameObject instRef, AssetData data)
    {
        itemData = data;
        instanceRef = instRef;
    }
}
