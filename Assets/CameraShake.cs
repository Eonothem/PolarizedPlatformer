using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.

	private float baseShake = 0;
	private float globalShake = 0;
	private float shakeTime = 0;
	private float addShake = 0;
	private float currentTime = 0;
	public float shakeFalloff = 0;

	Vector2 originPosition;
	Vector3 og;
	void Awake(){
		//shakeCamera (5f);
		//originPosition = transform.position;
		og = transform.position;
	}
	void Update ()
	{
		shake();
	}


	//Camera Shake
	//------------------------------
	void shake(){

		//Shakes the Camera
		originPosition = transform.position;
		Vector3 shakeVector = (originPosition +  Random.insideUnitCircle * globalShake);
		shakeVector.z = transform.position.z;
		transform.position = shakeVector;

		//How timed shakes work
		//1: baseShake = n;
		//2: Since globalShake becomes less than base shake, it does not stop the shake in the *.95 method
		//3: When the timer hits the end of shake time, it sets the base shake to 0
		//4: Then the *.95 method starts calling again, until it reaches 0
		if (currentTime >= shakeTime) {
			currentTime = 0;
			baseShake = 0;
		}

		//Keeps globalShake around the baseShake amount
		if(globalShake > baseShake) {
			globalShake *= shakeFalloff;
		}

		//Because lol unity and 1.234*10^-24
		if (globalShake <= .001) {
			globalShake = 0;
			transform.position = og;
		}
	}

	//Shakes for a specificied time [Good for events]
	public void shakeCamera(float intensity, float time){
		currentTime = 0;
		shakeTime = time;
		baseShake = intensity;
		globalShake += baseShake;
	}

	//Just adds more shake which begins to dampen immediatly [Good for hits/damage/etc]
	public void shakeCamera(float intensity){
		addShake = intensity;
		globalShake += addShake;
	}
}
