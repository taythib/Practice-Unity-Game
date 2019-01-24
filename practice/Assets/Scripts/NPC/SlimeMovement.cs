using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour {
	public float speed;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

	// Animator object
	private Animator anim;

    // bool signifying if moving and moving behavior
    private bool isHoming;
    private bool isIdle;
	private bool moving;

    // collision bools
    private bool colLeft = false;
    private bool colRight = false;
    private bool colUp = false;
    private bool colDown = false;

    private float moveCounter;
	private Vector3 direction;

    private void Awake()
    {
        speed = 3;
    }
    // Use this for initialization
    void Start () {
		moving = false;
        isIdle = true;
        isHoming = false;
        rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
        direction = new Vector2(0, 0);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (ContactPoint2D cp2d in coll.contacts)
        {
            //Debug.Log(cp2d.normal.x > 0 && rb2d.velocity.x > 0f);
            //Debug.Log(rb2d.velocity.x > 0f);
            //Debug.Log(cp2d.normal.x > 0);
            //Debug.Log(cp2d.normal.x < 0);
            //Debug.Log("colUp:" + colUp);
            //Debug.Log("colDown:" + colDown);
            //Debug.Log("colLeft:" + colLeft);
            //Debug.Log("colRight:" + colRight);
            //Debug.Log("-----------------------------");

            moving = false;
            //direction = new Vector2(0f, 0f);
            rb2d.velocity = direction;
            if (cp2d.normal.x > 0)
            {
                //direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colLeft = true;
                colRight = false;
            }
            if (cp2d.normal.x < 0)
            {
                //direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colRight = true;
                colLeft = false;
            }
            if (cp2d.normal.y > 0)
            {
                //direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colDown = true;
                colUp = false;
            }
            if (cp2d.normal.y < 0)
            {
                //direction = new Vector2(0f, 0f);
                rb2d.velocity = direction;
                colUp = true;
                colDown = false;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
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
        if (isIdle)
        {
            if (moving)
            {
                rb2d.velocity = direction;
                moveCounter -= Time.deltaTime;
                if (moveCounter < 0)
                {
                    moving = false;
                    moveCounter = 3;
                }
            }
            else
            {
                moveCounter -= Time.deltaTime;
                if (moveCounter < 0)
                {
                    selectDirection();
                }
            }
        }
        else if(isHoming)
        {
            rb2d.velocity = direction;
        }
	}

    public void moveToward(GameObject target, int speed)
    {
        isHoming = true;
        isIdle = false;
        this.speed = speed;
        direction = (target.transform.position - transform.position).normalized * speed;
    }

	void selectDirection (){
        direction = new Vector3(Random.Range(-1f, 1f) * speed, Random.Range(-1f, 1f) * speed, 0f);
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
