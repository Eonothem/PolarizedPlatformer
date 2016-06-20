using UnityEngine;
using System.Collections;

public class ReflectorControl : MonoBehaviour {
	Animator a;
	public AudioClip regularHit;
	public AudioClip perfectParry;
	public AudioClip reflectorUp;
	//float timeReflected = 0;
	int framesReflected = 0;
	public int perfectParryFrameTime = 5;
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if () {
		bool reflectDown = Input.GetKey(KeyCode.Space);
		if(Input.GetKeyDown(KeyCode.Space)){

			gameObject.GetComponent<AudioSource> ().PlayOneShot (reflectorUp);
		}

		if (reflectDown) {
			framesReflected += 1;
		} else {
			framesReflected = 0;
		}
		//Debug.Log (d);
		a.SetBool ("isActive", reflectDown);
		//}

		//Debug.Log (timeReflected);

	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Projectile") {
			//Debug.Log ("AAA");
			//Debug.Log(framesReflected);
			Vector2 vel = coll.gameObject.GetComponent<Rigidbody2D> ().velocity;
			gameObject.GetComponent<AudioSource> ().PlayOneShot (regularHit);
			//Debug.Log (vel);
			if (framesReflected < perfectParryFrameTime) {
				Debug.Log ("Perfect Reflect!");
				//Time.timeScale = 0.2f;
				//Time.fixedDeltaTime*=0.2f;
				gameObject.GetComponent<AudioSource> ().PlayOneShot (perfectParry);
				coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-vel.x+10, vel.y);

			} else {
				Debug.Log ("Regular Reflect.");
				coll.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-vel.x, vel.y);
			}
		}
	}
}

