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
	}

    void OnTriggerEnter2D(Collider2D coll) {
        GameObject obj = coll.gameObject;
        if (obj.layer == LayerMask.NameToLayer("Moveable")) {
            Debug.Log("Warp activated");
            if (exit != null) {
                coll.gameObject.transform.position = exit.transform.position;
            } else {
                coll.gameObject.transform.position = warpPoint;
            } 
        }
    }
}
