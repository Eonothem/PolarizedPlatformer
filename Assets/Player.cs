using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable {

    public int health = 1;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Rigidbody2D rigid;
	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	bool onPlatform = false;
	GameObject currentPlat = null;

    //HealthManager hm = null;
	
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
        //hm = gameObject.GetComponent<HealthManager>();
    }
	
	void Update () {
	}
	
	void FixedUpdate ()
	{
		//Checks if we hit the ground or not using bounding box magic
		BoxCollider2D bc = transform.GetComponentInParent<BoxCollider2D> ();
		float groundHeight = 1f;
		grounded = Physics2D.OverlapArea (new Vector2 (transform.position.x-bc.bounds.extents.x, transform.position.y), new Vector2 (transform.position.x+bc.bounds.extents.x, transform.position.y-groundHeight), whatIsGround);
		

		//Get Input
		float move = Input.GetAxis ("Horizontal");

		//Jump
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			rigid.velocity = new Vector2(rigid.velocity.x, 10);
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
		if (coll.gameObject.tag == "Platform") {
			currentPlat = coll.gameObject;
			onPlatform = true;
           transform.parent = coll.gameObject.transform;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		//When we leave the platform, unparent
		if (coll.gameObject.tag == "Platform") {
			currentPlat = null;
			onPlatform = false;
            transform.parent = null;
		}
	}

    void OnTriggerStay2D(Collision2D coll) {
        if (coll.gameObject.tag == "Hazard") {
            StageHazard haz = coll.gameObject.GetComponent<StageHazard>();
            damage(haz.damage);
        }
    }

    public void damage(int damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            kill();
        }
    }

    public void kill() {
        //death stuff
    }
} 