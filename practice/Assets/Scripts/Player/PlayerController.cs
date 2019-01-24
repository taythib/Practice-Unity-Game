using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private BoxCollider2D col;

	// Animator object attached to player
	private Animator anim; 

	// bool signifying if player is moving
	private bool moving;

	private Vector2 lastMove;

    private Collider2D objCol;

	private GameObject mainCamera;
	private GameObject playerCamera;
	private Button openEditorButton;
    private RectTransform overlayPanel;
    private GameObject gameController;
    private float maxTransform;
    private float minTransform;

    public static bool cameraControl;

    private GameObject heldOb;
    private bool isHoldingItem = false;
    private bool isHoldingSlime = false;



    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D> ();
        col = GetComponent<BoxCollider2D>();

		anim = GetComponent<Animator>();

        // Sets cameras, town editor button
		mainCamera = GameObject.Find("Main Camera");
		playerCamera = GameObject.Find("Player Camera");
		openEditorButton = GameObject.Find("Button").GetComponent<Button>();
		openEditorButton.onClick.AddListener(onClick);
        overlayPanel = GameObject.Find("OverlayPanel").GetComponent<RectTransform>();
        maxTransform = overlayPanel.transform.position.x - 280;
        minTransform = overlayPanel.transform.position.x;
        cameraControl = false;

        lastMove = new Vector2(0f, 0f);

        gameController = GameObject.Find("GameController");


        //Ray ray = new Ray(transform.position, Vector3.up);

    }

    //
    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    // Note to future self: If you have any collision issues in the future, try moving all raycasts to LateUpdate(). They might be rendering before the new player velocity has been taken into account ;)
    //
    void FixedUpdate()
    {
        KeyHandler();

        Rect box = new Rect(
            col.bounds.min.x,
            col.bounds.min.y,
            col.bounds.size.x,
            col.bounds.size.y
        );

        // perform some bit bullshit here to get the usable layer value
        int layer1 = 8;
        int layermask1 = 1 << layer1;

        // Each direction gets 3 ray checks: 2 on the outside for collisions, one in the middle for player interaction
        RaycastHit2D hitUp = Physics2D.Raycast(new Vector3(box.xMin, box.yMax, transform.position.z), new Vector3(0, 0.1f, 0), box.height / 8, layermask1);
        RaycastHit2D hitUpMid = Physics2D.Raycast(new Vector3(box.xMin + box.width / 2, box.yMax, transform.position.z), new Vector3(0, 0.3f, 0), box.height / 8, layermask1);
        RaycastHit2D hitUp2 = Physics2D.Raycast(new Vector3(box.xMax, box.yMax, transform.position.z), new Vector3(0, 0.1f, 0), box.height / 8, layermask1);

        RaycastHit2D hitDown = Physics2D.Raycast(new Vector3(box.xMin, box.yMin, transform.position.z), new Vector3(0, -0.1f, 0), box.height / 8, layermask1);
        RaycastHit2D hitDownMid = Physics2D.Raycast(new Vector3(box.xMin + box.width / 2, box.yMin, transform.position.z), new Vector3(0, -0.3f, 0), box.height / 8, layermask1);
        RaycastHit2D hitDown2 = Physics2D.Raycast(new Vector3(box.xMax, box.yMin, transform.position.z), new Vector3(0, -0.1f, 0), box.height / 8, layermask1);

        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector3(box.xMin, box.yMax, transform.position.z), new Vector3(-0.1f, 0, 0), box.height / 8, layermask1);
        RaycastHit2D hitLeftMid = Physics2D.Raycast(new Vector3(box.xMin, box.yMin + box.height / 2, transform.position.z), new Vector3(-0.3f, 0, 0), box.height / 8, layermask1);
        RaycastHit2D hitLeft2 = Physics2D.Raycast(new Vector3(box.xMin, box.yMin, transform.position.z), new Vector3(-0.1f, 0, 0), box.height / 8, layermask1);

        RaycastHit2D hitRight = Physics2D.Raycast(new Vector3(box.xMax, box.yMax, transform.position.z), new Vector3(0.1f, 0, 0), box.height / 8, layermask1);
        Debug.DrawRay(new Vector3(box.xMax, box.yMin + box.height / 2, transform.position.z), new Vector3(0.3f, 0, 0), Color.red, box.height / 8);
        RaycastHit2D hitRightMid = Physics2D.Raycast(new Vector3(box.xMax, box.yMin + box.height / 2, transform.position.z), new Vector3(0.3f, 0, 0), box.height / 8);
        RaycastHit2D hitRight2 = Physics2D.Raycast(new Vector3(box.xMax, box.yMin, transform.position.z), new Vector3(0.1f, 0, 0), box.height / 8, layermask1);

        // Player movement if town editor is not active
        // Checks all raycasts to see if they're hitting anything from Entities layer
        moving = false;
		if(!cameraControl){
			if (Input.GetAxisRaw("Horizontal") > 0.5f)
			{
                if (!((hitRight.collider && hitRight.collider.gameObject.layer == 8) || (hitRight2.collider && hitRight2.collider.gameObject.layer == 8)))
                {
                    transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f));
                }
                moving = true;
				lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
			}
            else if(Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                if(!((hitLeft.collider && hitLeft.collider.gameObject.layer == 8) || (hitLeft2.collider && hitLeft2.collider.gameObject.layer == 8)))
                {
                    transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f));
                }
                moving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }
            else
            {
                rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f)
			{
                if (!((hitUp.collider && hitUp.collider.gameObject.layer == 8) || (hitUp2.collider && hitUp2.collider.gameObject.layer == 8)))
                {
                    transform.Translate(new Vector2(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime));
                }
                moving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
			}
            else if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                if (!((hitDown.collider && hitDown.collider.gameObject.layer == 8) || (hitDown2.collider && hitDown2.collider.gameObject.layer == 8)))
                {
                    transform.Translate(new Vector2(0f, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime));
                }
                moving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            }

            if (hitRightMid.collider && hitRightMid.collider.gameObject.layer == 10)
            {
                Debug.Log("Collider:" + hitRightMid.collider);
            }

        }

        if (cameraControl && overlayPanel.transform.position.x > maxTransform)
        {
            overlayPanel.Translate(-40, 0, 0);
        }
        if (!cameraControl && overlayPanel.transform.position.x < minTransform)
        {
            overlayPanel.Translate(40, 0, 0);
        }

        // sets parameters for movement animations
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("LastMoveX", lastMove[0]);
        anim.SetFloat("LastMoveY", lastMove[1]);
        anim.SetBool("Moving", moving);
    }

    private void LateUpdate()
    {
        if (heldOb)
        {
            heldOb.transform.position = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);
            heldOb.GetComponent<SpriteRenderer>().sortingOrder = gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }

    }

    // toggles whether the town editor is active or not
    void onClick(){
		cameraControl = !cameraControl;
		mainCamera.transform.position = playerCamera.transform.position;
		mainCamera.SetActive(cameraControl);
		playerCamera.SetActive(!cameraControl);
        SaveLoad.Save();
    }

    // Mostly used to for tracking triggers with interaction layer
    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAH");

        if ((lastMove[1] > 0.5f && col.gameObject.transform.position.y > transform.position.y)
         || (lastMove[1] < -0.5f && col.gameObject.transform.position.y < transform.position.y)
         || (lastMove[0] > 0.5f && col.gameObject.transform.position.x > transform.position.x)
         || (lastMove[0] < -0.5f && col.gameObject.transform.position.x < transform.position.x))
        {
            objCol = col;
        }
        else
        {
            objCol = null;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        FlagManager.showDialogue = false;
    }

    private void KeyHandler()
    {
        if (objCol)
        {
            if (Input.GetKeyDown("space"))
            {
                FlagManager.showDialogue = true;
            }
            else if (Input.GetKeyDown("j") && !isHoldingItem && !isHoldingSlime && objCol.transform.parent.gameObject.layer != 8)
            {
                heldOb = new GameObject();
                heldOb.AddComponent<ItemDataManager>();
                heldOb.GetComponent<ItemDataManager>().itemData = objCol.transform.parent.gameObject.GetComponent<ItemDataManager>().itemData;
                heldOb.AddComponent<SpriteRenderer>();
                heldOb.GetComponent<SpriteRenderer>().sprite = objCol.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite;
                ManageItems.removeItem(objCol.transform.parent.gameObject);
                isHoldingItem = true;
                return;
            }
            else if (Input.GetKeyDown("j") && !isHoldingSlime && !isHoldingItem && objCol.transform.parent.gameObject.layer == 8)
            {
                heldOb = new GameObject();
                heldOb.AddComponent<SlimeDataManager>();
                heldOb.GetComponent<SlimeDataManager>().setActive(false);
                heldOb.GetComponent<SlimeDataManager>().slimeData = objCol.transform.parent.gameObject.GetComponent<SlimeDataManager>().slimeData;
                heldOb.AddComponent<SpriteRenderer>();
                heldOb.GetComponent<SpriteRenderer>().sprite = objCol.transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite;
                ManageSlime.removeSlime(objCol.transform.parent.gameObject);
                isHoldingSlime = true;
                return;
            }
        }
        if (Input.GetKeyDown("j") && isHoldingItem)
        {
            isHoldingItem = false;
            ManageItems.createItem(transform.position.x + 1.5f, transform.position.y, heldOb.GetComponent<ItemDataManager>().itemData);
            Destroy(heldOb);
            return;
        }
        else if (Input.GetKeyDown("j") && isHoldingSlime)
        {
            isHoldingSlime = false;
            ManageSlime.createSlime(transform.position.x + 1.5f, transform.position.y, heldOb.GetComponent<SlimeDataManager>().slimeData);
            Destroy(heldOb);
            return;
        }
    }
}