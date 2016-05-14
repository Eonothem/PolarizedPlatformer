using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParentPlatform: MonoBehaviour, IPolarizeable{
    private int IGNORE_PLAYER_MASK_FLAG = -257;
    

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
        LevelManager.getInstance().addPolarizeable(this as IPolarizeable);
	}

    
	
	// Update is called once per frame
	void Update () {


		//Make the path go to each waypoint target linearly, forever
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

    public void onNotifyPolarize(int polarizeMode)
    {
        int filterMode = LevelManager.getInstance().getFilterMode();
        //Debug.Log(filterMode);
        //Kill all shadows
        //Remove player parenting
        Debug.Log(polarizeMode);
        Transform shadowPlatform = masterPlatform.Find("Shadow(Clone)");
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 1f);

        if (shadowPlatform != null)
        {
            if (shadowPlatform.Find("Player") != null)
            {
                shadowPlatform.Find("Player").parent = null;
            }
            Destroy(shadowPlatform.gameObject);
        }

        if (polarizeMode != 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f,0f,0f,0.2f);

            GameObject newShadowPlatform = (GameObject)Instantiate(shadowPrefab, transform.position, Quaternion.identity);
            newShadowPlatform.GetComponent<ChildPlatform>().parentPlatform = transform;
            newShadowPlatform.transform.parent = masterPlatform;
            gameObject.GetComponent<PlatformEffector2D>().colliderMask = IGNORE_PLAYER_MASK_FLAG;
            newShadowPlatform.GetComponent<ChildPlatform>().type = polarizeMode;
        }
        else
        {
            gameObject.GetComponent<PlatformEffector2D>().colliderMask = -1;
        }
    }

	//Custom method for determining if the platform is at the waypoint
	//Might have to add errorbands in the future if we lerp the platform instead of directly just going to it
    bool isAtTarget()
    {
        return transform.position.x == currentWaypointTarget.position.x && transform.position.y == currentWaypointTarget.position.y;
    }

	/*
    public void getPolarized()
    {
        int filterMode = LevelManager.getInstance().getFilterMode();
        //Debug.Log(filterMode);
        //Debug.Log(gameObject.GetComponent<PlatformEffector2D>().colliderMask);
        if (filterMode == 1)
        {
            if (masterPlatform.Find("Shadow(Clone)") == null)
            {
                GameObject shadowPlatform = (GameObject)Instantiate(shadowPrefab, transform.position, Quaternion.identity);
                
                shadowPlatform.GetComponent<ChildPlatform>().parentPlatform = transform;
                shadowPlatform.GetComponent<ChildPlatform>().type = 1;
                shadowPlatform.transform.parent = masterPlatform;
                gameObject.GetComponent<PlatformEffector2D>().colliderMask = IGNORE_PLAYER_MASK_FLAG;
            }
        }else if (filterMode == 0)
        {
            gameObject.GetComponent<PlatformEffector2D>().colliderMask = -1;
            Transform horizontalShadow = masterPlatform.Find("Shadow(Clone)");
            
            if(horizontalShadow != null){
                
                if (horizontalShadow.Find("Player") != null)
                {
                    horizontalShadow.Find("Player").parent = null;
                }

                Destroy(horizontalShadow.gameObject);
            }

        }
        else if (filterMode == 2)
        {
            if (masterPlatform.Find("Shadow(Clone)") == null)
            {
                GameObject shadowPlatform = (GameObject)Instantiate(shadowPrefab, transform.position, Quaternion.identity);
                shadowPlatform.GetComponent<ChildPlatform>().parentPlatform = transform;
                shadowPlatform.GetComponent<ChildPlatform>().type = 2;
                shadowPlatform.transform.parent = masterPlatform;
                gameObject.GetComponent<PlatformEffector2D>().colliderMask = IGNORE_PLAYER_MASK_FLAG;
            }
        }
    }
    */
}
