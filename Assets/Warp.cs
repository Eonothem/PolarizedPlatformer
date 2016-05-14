using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
    public Vector2 warpPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("Warp activated");
        coll.gameObject.transform.position = warpPoint;
    }
}
