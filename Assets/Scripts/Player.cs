using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable {

    public const int maxHealth = 3;
    int health = maxHealth;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Rigidbody2D rigid;
	bool grounded = false;
	//public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	bool onPlatform = false;
	GameObject currentPlat = null;
    public Vector2 respawn = new Vector2((float)-9.5, (float)-2.1);
	public GameObject groundCheck;

    //HealthManager hm = null;
	void Awake(){
		//QualitySettings.vSyncCount = 0;
		//Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();

        //hm = gameObject.GetComponent<HealthManager>();
    }
	
	void Update () {
		//Debug.Log (groundCheck.GetComponent<GroundCheck> ().getGrounded ());
	}
	
	void FixedUpdate ()
	{	
		//Debug.Log (grounded);
		//Checks if we hit the ground or not using bounding box magic
		//BoxCollider2D bc = transform.GetComponentInParent<BoxCollider2D> ();
		//float groundHeight = 1f;
		//grounded = Physics2D.OverlapArea (new Vector2 (transform.position.x-bc.bounds.extents.x, transform.position.y), new Vector2 (transform.position.x+bc.bounds.extents.x, transform.position.y-groundHeight), whatIsGround);
		grounded = groundCheck.GetComponent<GroundCheck>().getGrounded();
		//Debug.Log (grounded);

		//Get Input
		float move = Input.GetAxis ("Horizontal");

		//Jump
		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			rigid.velocity = new Vector2(rigid.velocity.x, 20);
		}

		if (!grounded) {
			
			if (Input.GetKeyDown(KeyCode.DownArrow)){
				//Debug.Log ("AAAAAA");
				rigid.velocity = new Vector2 (rigid.velocity.x, -20);
			}
		}

		//Move left and right and be affected by gravity
		rigid.velocity = new Vector2 (move*maxSpeed, rigid.velocity.y);

		//Change direction
		if (move > 0 && !facingRight) {
			flip();
		}else if(move < 0 && facingRight){
			flip();
		}
	}

	void flip(){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//When we fall on a platform, parent our character to it so that it moves along with the platform
		//Debug.Log("Ayy");
		if (coll.gameObject.tag == "Platform") {
			currentPlat = coll.gameObject;
			onPlatform = true;
           transform.parent = coll.gameObject.transform;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		//Debug.Log ("LMAO");
		//When we leave the platform, unparent
		if (coll.gameObject.tag == "Platform") {
			currentPlat = null;
			onPlatform = false;
            transform.parent = null;
		}
	}

    void OnTriggerEnter2D(Collider2D coll) {
        //Debug.Log("Player collided with trigger");
        if (coll.gameObject.tag == "Hazard") {
            StageHazard haz = coll.gameObject.GetComponent<StageHazard>();
            damage(haz.damage);
        }
    }

    public void damage(int damage) {
        health -= damage;
        Debug.Log("Took " + damage + " damage");
        if (health <= 0) {
            health = 0;
            kill();
        }
    }

    public void kill() {
        Debug.Log("Died!");
        transform.position = respawn;
        rigid.velocity.Set(0, 0);

        health = maxHealth;
    }
} 