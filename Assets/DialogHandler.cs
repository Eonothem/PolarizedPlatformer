using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour {
	private CircleCollider2D trigger;
	private bool inRadius;
	private bool talking;
	// Use this for initialization
	void Start () {
		trigger = gameObject.GetComponent<CircleCollider2D> ();
		inRadius = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (inRadius);
		if (inRadius && !talking && Input.GetKeyDown (KeyCode.E)) {
			//Debug.Log ("Talk!");
			activateDialogBox();
			talking = true;
		}
	}

	private void activateDialogBox(){
		Debug.Log ("Talking!");
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "Player") {
			inRadius = true;

		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.tag == "Player") {
			inRadius = false;
		}
	}
}
