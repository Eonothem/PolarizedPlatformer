using UnityEngine;
using System.Collections;

public class StageHazard : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int damage;

    void OnTriggerEnter2D(Collider2D coll) {
        //HealthManager hm = coll.gameObject.GetComponent<HealthManager>();
        //hm.damage(damage);
    }
}
