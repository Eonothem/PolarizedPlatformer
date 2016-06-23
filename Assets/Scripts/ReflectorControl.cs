using UnityEngine;
using System.Collections;

public class ReflectorControl : MonoBehaviour {
	public GameObject player;

	Animator a;

	public GameObject playerAudioManager;
	private AudioSource playerAudioSource;
	private PlayerAudioFiles playerAudioFiles;

	int framesReflected = 0;
	public int perfectParryFrameTime = 5;

	//float initFixedTS = Time.fixedDeltaTime;


	// Use this for initialization
	void Start () {
		a = GetComponent<Animator> ();
		Debug.Log (Time.fixedDeltaTime);
		playerAudioSource = playerAudioManager.GetComponent<AudioSource>();
		playerAudioFiles = playerAudioManager.GetComponent<PlayerAudioFiles> ();

		Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update () {
		
		bool reflectDown = Input.GetKey(KeyCode.Space);

		if (reflectDown) {
			framesReflected += 1;
		} else {
			framesReflected = 0;
		}
		a.SetBool ("isActive", reflectDown);

	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Projectile") {
			

			Vector2 norm = coll.contacts[0].normal;

			playerAudioSource.PlayOneShot(playerAudioFiles.reflect);

			if (framesReflected < perfectParryFrameTime) {
				
				playerAudioSource.PlayOneShot(playerAudioFiles.perfectReflect);

				GameObject.Find ("Main Camera").GetComponent<CameraShake> ().shakeCamera (0.3f);
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(-norm*1200f);
				StartCoroutine ("MatrixEffect");

				//Debug.Log(-norm);


			} else {
				coll.gameObject.GetComponent<Rigidbody2D>().AddForce(-norm*800f);
			}
		}
	}

	IEnumerator MatrixEffect() {
		//float newTimeScale = 0.005f;
		//AudioSource a = GameObject.Find ("MusicManager").GetComponent<AudioSource>();

		//Wait two frames before we start the effect so that the physics can register
		for (int i = 0; i < 2; i++) {
			yield return null;
		}

		for (float f = 0.005f; f < 1f; f *= 1.2f) {
			
			Time.timeScale = f;
			Time.fixedDeltaTime*=f;
			yield return null;

		}

		//a.pitch = 1;
		//Time.timeScale = 1;
		resetTimeScale();
		//Time.fixedDeltaTime*=1;
	}

	public void resetTimeScale(){
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
	}
}

