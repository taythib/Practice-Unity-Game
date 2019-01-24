using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuilderView : MonoBehaviour {
	public GameObject currentBuild;
	private GameObject objectToBuild;

	// Use this for initialization
	void Start () {
		currentBuild = Resources.Load("House") as GameObject;
		objectToBuild = Instantiate(currentBuild, transform.position, Quaternion.identity);
	}
	
	// Converts mouse position to World View coordinates, then applies to the to-be built object's position
	void Update () {
		if(PlayerController.cameraControl && FlagManager.assetSelected){
			Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height, 10.0f));
			objectToBuild.transform.position = new Vector3 (Mathf.Round(pos.x) - 0.5f, Mathf.Round(pos.y) - 0.5f, pos.z);
		} else
        {
            objectToBuild.transform.position = new Vector3(0f, 0f, -1000f);
        }

		if(Input.GetMouseButtonDown(0) && PlayerController.cameraControl)
        {
			Instantiate(currentBuild, objectToBuild.transform.position, Quaternion.identity);
		}
	}
}
