using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
    public Vector2 warpPoint;
	public Transform exit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (exit == null);
	}

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Warp activated");
		if (exit != null) {
			coll.gameObject.transform.position = exit.transform.position;
		} else {
			coll.gameObject.transform.position = warpPoint;
		}
    }
}
