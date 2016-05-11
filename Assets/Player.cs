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
		//collide = GetComponent<BoxCollider2D> ();
		rigid = GetComponent<Rigidbody2D> ();
        //hm = gameObject.GetComponent<HealthManager>();
    }
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKey (KeyCode.A)) {
		//	movex = -1;
		//} else if (Input.GetKey (KeyCode.D)) {
		//	movex = 1;
		//} else {
		//	movex = 0;
		//}
		//if (Input.GetKey (KeyCode.W))
			//GetComponent<Rigidbody2D>().AddForce(new Vector2(0,100));
		//else
			//movey = 0;
	}
	
	void FixedUpdate ()
	{
		BoxCollider2D bc = transform.GetComponentInParent<BoxCollider2D> ();
		//Debug.Log (bc.bounds.extents.x);
		float groundHeight = 1f;
		grounded = Physics2D.OverlapArea (new Vector2 (transform.position.x-bc.bounds.extents.x, transform.position.y), new Vector2 (transform.position.x+bc.bounds.extents.x, transform.position.y-groundHeight), whatIsGround);
		//grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		//Debug.Log (transform.position.x + bc.bounds.extents.x);
		//Debug.Log (grounded);
		//Vector2 prevVelocity = GetComponent<Rigidbody2D> ().velocity;
		//GetComponent<Rigidbody2D> ().velocity = new Vector2 (movex*Speed, prevVelocity.y);
		float move = Input.GetAxis ("Horizontal");

		float platformAddX = 0;
		float platformAddY = 0;

		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			rigid.velocity = new Vector2(rigid.velocity.x, 10);
		}

		if (onPlatform) {
			platformAddX = currentPlat.GetComponent<Rigidbody2D>().velocity.x;
		}

		rigid.velocity = new Vector2 (move*maxSpeed+platformAddX, rigid.velocity.y);

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
		if (coll.gameObject.tag == "Platform") {
			currentPlat = coll.gameObject;
			onPlatform = true;
			//Debug.Log("PLat");
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Platform") {
			currentPlat = null;
			onPlatform = false;
			//Debug.Log("PsdfsLat");
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