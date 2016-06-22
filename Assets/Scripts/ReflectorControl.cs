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
	public GameObject player;
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator> ();
		//Time.timeScale = 0.1f;
		//Time.fixedDeltaTime*=0.1f;
		//game
		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
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

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Projectile") {
			

			Vector2 norm = coll.contacts[0].normal;

			gameObject.GetComponent<AudioSource> ().PlayOneShot (regularHit);
		
			if (framesReflected < perfectParryFrameTime) {
				
				gameObject.GetComponent<AudioSource> ().PlayOneShot (perfectParry);


				GameObject.Find ("Main Camera").GetComponent<CameraShake> ().shakeCamera (0.3f);
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(-norm*1200f);


			} else {
				//Debug.Log ("Regular Reflect.");
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(-norm*800f);
			}
		}
	}

	IEnumerator MatrixEffect() {
		//float newTimeScale = 0.005f;
		AudioSource a = GameObject.Find ("MusicManager").GetComponent<AudioSource>();

		for (float f = 0.005f; f < 1f; f *= 1.2f) {
			//if (Mathf.Abs(f - 0.01f) == 1f) {
				//f == 1f;
			//}
			Debug.Log (f);
			a.pitch = f;
			Time.timeScale = f;
			Time.fixedDeltaTime*=f;
			yield return null;

		}

		a.pitch = 1;
		Time.timeScale = 1;
		Time.fixedDeltaTime*=1;
	}
}

