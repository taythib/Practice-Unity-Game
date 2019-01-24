using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {
	private float speed;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

	// Animator object
	private Animator anim; 

	// bool signifying if moving
	private bool moving;
    private bool colLeft = false;
    private bool colRight = false;
    private bool colUp = false;
    private bool colDown = false;

    private float moveCounter;
	private int moveRand;
	private int direction;

	// Use this for initialization
	void Start () {
		moving = false;
        speed = 3;
        rb2d = GetComponent<Rigidbody2D> ();
		moveCounter = 1;
		anim = GetComponent<Animator>();
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (ContactPoint2D cp2d in coll.contacts)
        {
            if (cp2d.normal.x > 0 && direction == 3){ 
                print("hit left");
                rb2d.velocity = new Vector2(0f, 0f);
                colLeft = true;
            }
            else if (cp2d.normal.x < 0 && direction == 2)
            { 
                print("hit right");
                rb2d.velocity = new Vector2(0f, 0f);
                colRight = true;
            }
            if (cp2d.normal.y > 0 && direction == 0)
            {
                print("hit down");
                rb2d.velocity = new Vector2(0f, 0f);
                colDown = true;
            }
            else if (cp2d.normal.y < 0 && direction == 1)
            {
                print("hit up");
                rb2d.velocity = new Vector2(0f, 0f);
                colUp = true;
            }
        }
        moveCounter -= Time.deltaTime;
        if (moveCounter < 0)
        {
            selectDirection();
            moveCounter = 5;
        }

        Debug.Log("hit");
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        colLeft = false;
        colRight = false;
        colUp = false;
        colDown = false;
    }

    // Update is called once per frame
    void Update () {

			if(moving){
			    // Debug.Log(moveCounter);
				moveCounter -= Time.deltaTime;
				switch(direction){
					case 0:
                    if (!colDown)
                    {
                        rb2d.velocity = new Vector2(0f, -speed);
                    }
                    break;
					case 1:
                    if (!colUp)
                    {
                        rb2d.velocity = new Vector2(0f, speed);
                    }
                    break;
					case 2:
                    if (!colRight)
                    {
                        rb2d.velocity = new Vector2(speed, 0f);
                    }
                    break;

                case 3:
                    if (!colLeft)
                    {
                        rb2d.velocity = new Vector2(-speed, 0f);
                    }
                    break;
            }
				if(moveCounter < 0){
					moving = false;
					moveCounter = 1;
				}
			}else{
				//Debug.Log(moveCounter);
				moveCounter -= Time.deltaTime;
				if(moveCounter < 0)
					selectDirection();
			}
	}

	void selectDirection (){
        //Debug.Log("Selecting...");
        int oldDirection = direction;
		direction = Random.Range(0,4);
        if(oldDirection != direction)
        {
            colLeft = false;
            colRight = false;
            colUp = false;
            colDown = false;
        }
		moveCounter = 3;
		moving = true;

	}
}
