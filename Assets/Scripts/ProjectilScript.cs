using UnityEngine;
using System.Collections;

public class ProjectilScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 10);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		

	}

	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("AA");

		if (coll.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
