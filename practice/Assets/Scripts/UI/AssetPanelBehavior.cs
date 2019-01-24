using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Changes color of button while hovering over it by calculating the rectangles corners and seeing if the mouse lies within it
public class AssetPanelBehavior : MonoBehaviour {
    private RectTransform assetButton;
    private Vector2 bottomLeft;
    private Vector2 bottomRight;
    private Vector2 topLeft;
    private Vector2 topRight;

    public int index;

    private bool selected = false;

    // Use this for initialization
    void Start () {
        assetButton = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update () {
        if (IsMouseOver() || selected)
        {
            assetButton.GetComponent<Image>().color = new Color(0F, 0F, 0F, 0.3F);
            if (Input.GetMouseButtonDown(0) && IsMouseOver())
            {
                selected = !selected;
                FlagManager.assetSelected = selected;
                gameObject.GetComponentInParent<AssetPanelManager>().checkSelected(index);
            }
        }
        else if(!selected)
            assetButton.GetComponent<Image>().color = new Color(0F, 0F, 0F, 0.6F);

    }

    public void setUnselected()
    {
        assetButton.GetComponent<Image>().color = new Color(0F, 0F, 0F, 0.6F);
    }

    private bool IsMouseOver()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2[] corners = new Vector2[4];
        corners[0] = new Vector2(assetButton.transform.position.x - assetButton.rect.width / 2, assetButton.transform.position.y - assetButton.rect.height / 2);
        corners[1] = new Vector2(assetButton.transform.position.x + assetButton.rect.width / 2, assetButton.transform.position.y - assetButton.rect.height / 2);
        corners[2] = new Vector2(assetButton.transform.position.x - assetButton.rect.width / 2, assetButton.transform.position.y + assetButton.rect.height / 2);
        corners[3] = new Vector2(assetButton.transform.position.x + assetButton.rect.width / 2, assetButton.transform.position.y + assetButton.rect.height / 2);


        if (mousePosition.x >= corners[0].x && mousePosition.x < corners[1].x
           && mousePosition.y >= corners[0].y && mousePosition.y < corners[2].y)
        {
            return true;
        }    
     return false;
    }
}
