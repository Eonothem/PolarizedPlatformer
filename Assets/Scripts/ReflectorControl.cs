using UnityEngine;
using System.Collections;

public class ReflectorControl : MonoBehaviour {
	Animator a;
	public float meme;
	// Use this for initialization
	void Start () {
		a = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if () {
		bool d = Input.GetKey(KeyCode.Space);
		//Debug.Log (d);
			a.SetBool ("isActive", d);
		//}
	}
}
