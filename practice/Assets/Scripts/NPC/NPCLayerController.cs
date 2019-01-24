using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLayerController : MonoBehaviour {
	private GameObject player;
	private bool playerIsAbove;
	private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		player =  GameObject.Find("Player");
		sprite = GetComponent<SpriteRenderer>();
		if(player.transform.position.y > transform.position.y){
			playerIsAbove = true;
		}else{
			playerIsAbove = false;
			sprite.sortingOrder--;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.y > transform.position.y && !playerIsAbove){
			sprite.sortingOrder++;
			playerIsAbove = !playerIsAbove;
		}else if(player.transform.position.y < transform.position.y && playerIsAbove){
			sprite.sortingOrder--;
			playerIsAbove = !playerIsAbove;
		}
	}
}
