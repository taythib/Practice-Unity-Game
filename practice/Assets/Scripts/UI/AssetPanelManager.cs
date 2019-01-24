using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Initializes AssetPanels in overlay and "equally" spaces them out
public class AssetPanelManager : MonoBehaviour {
    GameObject[] assetSlots;
    private float startHeight = 652.97f;
    private Sprite[] Sprites;

    // Use this for initialization
    void Start () {
        assetSlots = new GameObject[3];
        for(int i = 0; i < assetSlots.Length; i++)
        {
            GameObject currentSlot = Resources.Load("AssetPanel") as GameObject;
            assetSlots[i] = Instantiate(currentSlot, new Vector3(transform.position.x, startHeight - i * 265, 0f), Quaternion.identity);
            assetSlots[i].GetComponent<AssetPanelBehavior>().index = i;
            assetSlots[i].transform.SetParent(transform);
            Sprites = Resources.LoadAll<Sprite>("Art/" + ManageAssets.items[i].spriteSheet);
            assetSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = Sprites[ManageAssets.items[i].sheetIndex];
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void checkSelected(int index)
    {
        for(int i = 0; i < assetSlots.Length; i++)
        {
            if (i != index)
                assetSlots[i].GetComponent<AssetPanelBehavior>().setUnselected();
        }
    }
}
