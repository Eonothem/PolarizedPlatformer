using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public GameObject healthBar;
	public GameObject reflector;
	public GameObject broken;
	public float lerpSpeed = 100f;

	private Image healthBarImage;
	private Image brokenImage;
	private ReflectorControl reflectorScript;
	private float reflectorPercentage;
	private float redFlash = 0;

	public Color reflectorFullHealth;
	public Color reflectorLowHealth;
	// Use this for initialization
	void Start () {
		//Debug.Log(reflectorFullHealth);
		healthBarImage = healthBar.GetComponent<Image> ();
		brokenImage = broken.GetComponent<Image> ();
		reflectorScript = reflector.GetComponent<ReflectorControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		reflectorPercentage = (float)reflectorScript.currentHealth / (float)reflectorScript.maxHealth;

		if (reflectorPercentage != healthBarImage.fillAmount) {
			healthBarImage.fillAmount = Mathf.Lerp (healthBarImage.fillAmount, reflectorPercentage, Time.deltaTime*lerpSpeed);

		}

		healthBarImage.color = Color.Lerp (reflectorLowHealth, reflectorFullHealth, reflectorPercentage);

		if (reflectorScript.broken) {
			
			brokenImage.color = new Color (1, 0, 0, (0.5f)*Mathf.Sin ((float)redFlash / 12)+(0.5f) );
			redFlash++;
		} else {
			brokenImage.color = new Color (1, 0, 0, 0);
			redFlash = 0;
		}
		//healthBarImage.color = reflectorFullHealth;
	}


}
