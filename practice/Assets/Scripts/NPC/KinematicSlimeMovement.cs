using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSlimeMovement : MonoBehaviour {
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
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		moving = false;
        speed = 7;
        rb2d = GetComponent<Rigidbody2D> ();
		moveCounter = 1;
		anim = GetComponent<Animator>();
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (ContactPoint2D cp2d in coll.contacts)
        {
            //Debug.Log(cp2d.normal.x > 0 && rb2d.velocity.x > 0f);
            //Debug.Log(rb2d.velocity.x > 0f);
            //Debug.Log(cp2d.normal.x > 0);
            //Debug.Log(cp2d.normal.x < 0);
            Debug.Log("colUp:" + colUp);
            Debug.Log("colDown:" + colDown);
            Debug.Log("colLeft:" + colLeft);
            Debug.Log("colRight:" + colRight);
            Debug.Log("-----------------------------");

            moving = false;
            direction = new Vector2(0f, 0f);
            rb2d.velocity = direction;
            if (cp2d.normal.x > 0 && rb2d.velocity.x < 0f && !colRight)
            {
                print("hit left");
                direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colLeft = true;
            }
            if (cp2d.normal.x < 0 && rb2d.velocity.x > 0f && !colLeft)
            {
                print("hit right");
                direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colRight = true;
            }
            if (cp2d.normal.y > 0 && rb2d.velocity.y < 0f && !colUp)
            {
                print("hit down");
                direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colDown = true;
            }
            if (cp2d.normal.y < 0 && rb2d.velocity.y > 0f && !colDown)
            {
                print("hit up");
                direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colUp = true;
            }
        }
        //moveCounter -= Time.deltaTime;
        //if (moveCounter < 0)
        //{
        //    selectDirection();
        //    moveCounter = 5;
        //}
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        Debug.Log("collision");
        if (colRight && rb2d.velocity.x < 0f)
        {
            colRight = false;
        }
        else if (colLeft && rb2d.velocity.x > 0f)
        {
            colLeft = false;
        }
        if (colUp && rb2d.velocity.y < 0f)
        {
            colUp = false;
        }
        else if (colDown && rb2d.velocity.y > 0f)
        {
            colDown = false;
        }
    }

    // Update is called once per frame
    void Update () {

			if(moving){
                rb2d.velocity = direction;
                moveCounter -= Time.deltaTime;
                if (moveCounter < 0){
					    moving = false;
					    moveCounter = 3;
			    }
			}else{
				moveCounter -= Time.deltaTime;
                if (moveCounter < 0)
                    {
                    selectDirection();
                    }
			}
	}

	void selectDirection (){
        Debug.Log("Selecting...");
        direction = new Vector3(Random.Range(-1f, 1f) * speed, Random.Range(-1f, 1f) * speed, 0f);
        
        //Debug.Log("colUp:" + colUp);
        //Debug.Log("colDown:" + colDown);
        //Debug.Log("colLeft:" + colLeft);
        //Debug.Log("colRight:" + colRight);
        while ((colLeft && direction.x < 0f) || (colRight && direction.x > 0f) || (colDown && direction.y < 0f) || (colUp && direction.y > 0f))
        {
            direction = new Vector3(Random.Range(-1f, 1f) * speed, Random.Range(-1f, 1f) * speed, 0f);
            //Debug.Log("colUp:" + colUp);
            //Debug.Log("colDown:" + colDown);
            //Debug.Log("colLeft:" + colLeft);
            //Debug.Log("colRight:" + colRight);
        }
        moveCounter = 3;
		moving = true;
	}
}
