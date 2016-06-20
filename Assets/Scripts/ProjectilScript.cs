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
		Destroy (gameObject);
	}
}
