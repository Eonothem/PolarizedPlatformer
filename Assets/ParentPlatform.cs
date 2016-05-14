using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParentPlatform : MonoBehaviour {
    public float speed;
    public List<Transform> waypoints;
    public Transform masterPlatform;
    public GameObject shadowPrefab;

    private Transform currentWaypointTarget;
    private int currentWaypointTargetNum;

	// Use this for initialization
	void Start () {
        currentWaypointTargetNum = 1;
        currentWaypointTarget = waypoints[currentWaypointTargetNum];
	}
	
	// Update is called once per frame
	void Update () {

        getPolarized();

        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, currentWaypointTarget.position, step);

        if (isAtTarget())
        {
            if (currentWaypointTargetNum + 1 == waypoints.Count)
            {
                currentWaypointTargetNum = 0;
            }
            else
            {
                currentWaypointTargetNum++;
            }

            currentWaypointTarget = waypoints[currentWaypointTargetNum];
        }


	}

    public void getPolarized()
    {
        int filterMode = LevelManager.getInstance().getFilterMode();
        //Debug.Log(filterMode);

        if (filterMode == 1)
        {
            if (masterPlatform.Find("Horizontal Shadow(Clone)") == null)
            {
                GameObject shadowPlatform = (GameObject)Instantiate(shadowPrefab, transform.position, Quaternion.identity);
                shadowPlatform.GetComponent<ChildPlatform>().parentPlatform = transform;
                shadowPlatform.transform.parent = masterPlatform;
            }
        }
    }

    bool isAtTarget()
    {
        return transform.position.x == currentWaypointTarget.position.x && transform.position.y == currentWaypointTarget.position.y;
    }
}
