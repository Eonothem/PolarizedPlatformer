using UnityEngine;
using System.Collections;

public class ChildPlatform : MonoBehaviour{
    public Transform parentPlatform;
    public int type;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (type == 1)
        {
            transform.position = new Vector2(parentPlatform.position.x, transform.position.y);
        }
        else if (type == 2)
        {
            transform.position = new Vector2(transform.position.x, parentPlatform.position.y);
        }
    }

    public void setParentPlatform(Transform t)
    {
        parentPlatform = t;
    }

    public void setType(int type){
        this.type = type;
    }
}
