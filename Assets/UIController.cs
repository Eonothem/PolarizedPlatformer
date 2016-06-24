using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public GameObject healthBar;
	public GameObject reflector;
	public float lerpSpeed = 100f;

	private Image healthBarImage;
	private ReflectorControl reflectorScript;
	private float reflectorPercentage;

	public Color reflectorFullHealth;
	public Color reflectorLowHealth;
	// Use this for initialization
	void Start () {
		//Debug.Log(reflectorFullHealth);
		healthBarImage = healthBar.GetComponent<Image> ();
		reflectorScript = reflector.GetComponent<ReflectorControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		reflectorPercentage = (float)reflectorScript.currentHealth / (float)reflectorScript.maxHealth;

		if (reflectorPercentage != healthBarImage.fillAmount) {
			healthBarImage.fillAmount = Mathf.Lerp (healthBarImage.fillAmount, reflectorPercentage, Time.deltaTime*lerpSpeed);

		}

		healthBarImage.color = Color.Lerp (reflectorLowHealth, reflectorFullHealth, reflectorPercentage);

		//healthBarImage.color = reflectorFullHealth;
	}
}
