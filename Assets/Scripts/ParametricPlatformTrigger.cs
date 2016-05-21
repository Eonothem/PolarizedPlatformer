using UnityEngine;
using System.Collections;

public class ParametricPlatformTrigger : MonoBehaviour {
	public GameObject platform;
	private ParentPlatformParametric pController;

	public float speed;
	public float xamp;
	public float xfreq = 1;
	public float xphase;
	public float xshift;
	public float yamp;
	public float yfreq = 1;
	public float yphase;
	public float yshift;
	private float[] initCondictions;

	// Use this for initialization
	void Start () {
		pController = platform.GetComponent<ParentPlatformParametric>();

		initCondictions = new float[] {speed, xamp, xfreq, xphase, xshift, yamp, yfreq, yphase, yshift };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log("Modifying Platform");
		pController.modify(initCondictions);
	}

	void OnTriggerExit2D(Collider2D coll) {
		Debug.Log("Exit");
		pController.reset();
	}
}
