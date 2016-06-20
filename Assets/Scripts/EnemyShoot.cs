using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
	public GameObject projectile;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Time.frameCount % 60 == 0){
			
			//Debug.Log("AAA");
			Vector2 newPoint = new Vector2 (transform.position.x - 5, transform.position.y);
			GameObject proj = (GameObject)Instantiate (projectile, newPoint, transform.rotation);
			proj.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-15, 0);


		}
	}
}
