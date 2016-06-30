using UnityEngine;
using System.Collections;

public class DialogHandler : MonoBehaviour {
	private CircleCollider2D trigger;
	private bool inRadius;
	private bool talking;
	public GameObject dialogUIObject;
	private bool crRunning = false;
	// Use this for initialization
	void Start () {
		trigger = gameObject.GetComponent<CircleCollider2D> ();
		inRadius = false;
	}
	
	// Update is called once per frame
	void Update () {
		//talking = dialogUIObject.GetComponent<TextScroller> ().active;
		if (!dialogUIObject.GetComponent<TextScroller> ().active && talking && !crRunning ) {
			Debug.Log ("AA");
			crRunning = true;
			StartCoroutine (endTalk());


		}
		if (inRadius && !talking && Input.GetKeyDown (KeyCode.E)) {
			//Debug.Log ("Talk!");
			activateDialogBox();
			talking = true;
		}
	}

	private void activateDialogBox(){
		//Debug.Log ("Talking!");
		dialogUIObject.GetComponent<TextScroller> ().activate();
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

	IEnumerator endTalk(){

		for (int i = 0; i < 30; i++)
		{
			//Debug.Log (i);
			//textBox.text = goatText[currentlyDisplayingText].Substring(0, i);
			yield return null;
		}
		crRunning = false;
		talking = false;
		//Debug.Log ("AfdsfA");
	}

}
