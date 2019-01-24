using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDataManager : MonoBehaviour {
    private GameObject instanceRef;
    private SlimeMovement movement;
    public SlimeData slimeData;
    private bool active = true;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        slimeData.Update();
        GameObject target = GameObject.FindWithTag("Item");
        if ((slimeData.hunger / 60) > 1 && target && active)
        {
            movement.moveToward(target, 10);
        }
    }

    public void SetData(GameObject instRef, SlimeData data)
    {
        slimeData = data;
        instanceRef = instRef;
        movement = instRef.GetComponent<SlimeMovement>();
        movement.speed = slimeData.speed;
    }

    public SlimeData GetData()
    {
        return this.slimeData;
    }

    public void setActive(bool a)
    {
        active = a;
    }
}
