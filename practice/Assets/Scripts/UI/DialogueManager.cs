using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        //if(!FlagManager.showDialogue);
        transform.GetChild(0).gameObject.SetActive(FlagManager.showDialogue);
	}
}
