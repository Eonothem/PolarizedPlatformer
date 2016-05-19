using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class move : MonoBehaviour {
    public float speed;
    public List<Transform> waypoints;
    private Transform currentWaypointTarget;
    private int currentWaypointTargetNum;
    public Transform parentPlatform;
    public int type;
	// Use this for initialization
	void Start () {
        currentWaypointTargetNum = 1;
        currentWaypointTarget = waypoints[currentWaypointTargetNum];

        if (type != 0)
        {
            speed = parentPlatform.gameObject.GetComponent<move>().speed;
        }
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;

        if (type == 0){
            transform.position = Vector2.MoveTowards(transform.position, currentWaypointTarget.position, step);

            if (isAtTarget())
            {
                if (currentWaypointTargetNum + 1 == waypoints.Count){
                    currentWaypointTargetNum = 0;
                }else{
                    currentWaypointTargetNum++;
                }

                currentWaypointTarget = waypoints[currentWaypointTargetNum];
            }

        }else if (type == 1) {
            transform.position = new Vector2(parentPlatform.position.x, transform.position.y);
        }
        else if (type == 2){
            transform.position = new Vector2(transform.position.x, parentPlatform.position.y);
        }
	}

    bool isAtTarget()
    {
        return transform.position.x == currentWaypointTarget.position.x && transform.position.y == currentWaypointTarget.position.y;
    }

}
