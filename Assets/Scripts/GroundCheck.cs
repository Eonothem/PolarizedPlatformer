using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {
	private bool grounded = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (grounded);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		//Debug.Log("AAAAAAAA");
		grounded = true;

	}

	void OnTriggerExit2D(Collider2D coll) {
		//Debug.Log("AAAAAAAA");
		grounded = false;
	}

	//void OnCollisionEnter2D(Collision2D coll) {
		//When we fall on a platform, parent our character to it so that it moves along with the platform
		//Debug.Log("Grounded");
		//grounded = true;
		//if (coll.gameObject.tag == "Platform") {
		//}
	//}

	//void OnCollisionExit2D(Collision2D coll) {
		//grounded = false;
	//}

	public bool getGrounded(){
		return grounded;
	}
}
