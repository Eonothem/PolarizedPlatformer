using UnityEngine;
using System.Collections;

public class ProjectilScript : MonoBehaviour {
	public int damage = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy (gameObject, 10);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag != "Reflector") {
			Destroy (gameObject);
		}
		//Debug.Log (coll.gameObject.tag);
	}

	void OnTriggerEnter2D(Collider2D coll){
		//Debug.Log ("AA");
	}
}
