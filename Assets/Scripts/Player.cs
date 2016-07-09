using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable {
	//public AudioClip hit;
	public GameObject playerAudioManager;
	private AudioSource playerAudioSource;
	private PlayerAudioFiles playerAudioFiles;


    public const int maxHealth = 3;
    int health = maxHealth;
	public float maxSpeed = 10f;
	bool facingRight = true;
	Rigidbody2D rigid;
	//float groundRadius = 0.2f;
	bool onPlatform = false;
	GameObject currentPlat = null;
    public Vector2 respawn = new Vector2((float)-9.5, (float)-2.1);
	public Transform groundCheckBack;
    public Transform groundCheckForward;
    bool grounded = false;
    //private bool goDown = false;
    private bool fastfall = false;
	private bool jumpTrigger = false;

	//Animator a;
	//bool down;

    //HealthManager hm = null;
	void Awake(){
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 30;
          groundCheckBack = transform.Find("groundCheckBack");
          groundCheckForward = transform.Find("groundCheckForward");
	}

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();

		playerAudioSource = playerAudioManager.GetComponent<AudioSource>();
		playerAudioFiles = playerAudioManager.GetComponent<PlayerAudioFiles> ();
		//a = GetComponent<Animator> ();
        //hm = gameObject.GetComponent<HealthManager>();
    }
	
	//void Update () {
		//Debug.Log (groundCheck.GetComponent<GroundCheck> ().getGrounded ());

	//}

	private float movementInput;
	void Update(){
		//Get Input

		movementInput = Input.GetAxis ("Horizontal");
		if (Input.GetKey (KeyCode.DownArrow) && rigid.velocity.y < 0) { fastfall = true; }
		if (Input.GetKey (KeyCode.UpArrow) && grounded){jumpTrigger = true;};

        grounded = Physics2D.Linecast(transform.position, groundCheckBack.position, 1 << LayerMask.NameToLayer("Ground")) ||
                   Physics2D.Linecast(transform.position, groundCheckForward.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.Log("grounded? " + grounded);

        //Change direction
        if (movementInput > 0 && !facingRight) {
			flip();
		}else if(movementInput < 0 && facingRight){
			flip();
		}

		if (grounded) {
			fastfall = false;
		} else {
			jumpTrigger = false;
		}
	}


	void FixedUpdate ()
	{	
		if (jumpTrigger && grounded) {
			rigid.velocity = new Vector2(rigid.velocity.x, 15);
		}
			
		if (!grounded && rigid.velocity.y < 0 && fastfall){
				rigid.velocity = new Vector2 (rigid.velocity.x, -20);

		}

		rigid.velocity = new Vector2 (movementInput*maxSpeed, rigid.velocity.y);


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

		if (coll.gameObject.tag == "Projectile") {
			damage (coll.gameObject.GetComponent<ProjectilScript> ().damage);
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
			StageHazard haz = coll.gameObject.GetComponent<StageHazard> ();
			damage (haz.damage);
		}
    }

    public void damage(int damage) {
        health -= damage;
        Debug.Log("Took " + damage + " damage");
        GameObject.Find("Main Camera").GetComponent<CameraShake>().shakeCamera(0.02f);
        if (health <= 0) {
            health = 0;
            kill();
        } else {
            playerAudioSource.PlayOneShot(playerAudioFiles.hurt);
        }

    }

    public void kill() {
        Debug.Log("Died!");
		playerAudioSource.PlayOneShot(playerAudioFiles.die);
        transform.position = respawn;
        rigid.velocity = Vector2.zero;

        health = maxHealth;
    }
} 