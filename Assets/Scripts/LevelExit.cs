using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour {

	public string sceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//When the player enters the trigger, go to the scene with that name
	//If you're getting in error, you first have to put the scene in the build path so just go to File->Build Settings and click add current scene to path
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log("Transitioning Level");
			SceneManager.LoadScene (sceneName);
		}
	}
}
