using UnityEngine;
using System.Collections;

public class ChildPlatform : MonoBehaviour {
    public Transform parentPlatform;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector2(parentPlatform.position.x, transform.position.y);
    }

    public void setParentPlatform(Transform t)
    {
        parentPlatform = t;
    }
}
