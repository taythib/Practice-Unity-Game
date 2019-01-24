using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour {
    private RectTransform assetButton;
    private  Vector2 bottomLeft;
    private Vector2 bottomRight;
    private Vector2 topLeft;
    private Vector2 topRight;


    // Use this for initialization
    void Start () {
        assetButton = GameObject.Find("AssetPanel").GetComponent<RectTransform>();
        bottomLeft = new Vector2(assetButton.transform.position.x, assetButton.transform.position.y);
        bottomRight = new Vector2(assetButton.transform.position.x + assetButton.rect.width, assetButton.transform.position.y);
        //topLeft = assetButton.transform.position.x;
        //topRight = assetButton.transform.position.x;
        Debug.Log(bottomLeft);
        Debug.Log(bottomRight);
        Debug.Log(Input.mousePosition.x);

    }

    // Update is called once per frame
 //   void Update () {
		
	//}

    public bool IsMouseOver()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3[] worldCorners = new Vector3[4];

        if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x
           && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
        {
            return true;
        }    
     return false;
    }
}
