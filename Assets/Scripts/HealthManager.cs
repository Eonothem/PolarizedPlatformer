using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public int health;
    
    public void damage(int damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            //kill();
        }
    }
}
