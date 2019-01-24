using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerController.cameraControl){
			if(Input.GetAxisRaw("Horizontal") == 1f)
			{
				transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
			}
			if(Input.GetAxisRaw("Horizontal") == -1f)
			{
				transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
			}
			if(Input.GetAxisRaw("Vertical") == -1f)
			{
				transform.Translate(new Vector3(0,-speed * Time.deltaTime,0));
			}
			if(Input.GetAxisRaw("Vertical") == 1f)
			{
				transform.Translate(new Vector3(0,speed * Time.deltaTime,0));
			}
		}   
	}
}
