using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParentPlatformParametric : MonoBehaviour, IPolarizeable {
    private int IGNORE_PLAYER_MASK_FLAG = -257;

    public float speed;
    public Transform masterPlatform;
    public GameObject shadowPrefab;
    float stepInc = 0f;

    public float xamp;
    public float xfreq = 1;
    public float xphase;
    public float xshift;
    public float yamp;
    public float yfreq = 1;
    public float yphase;
    public float yshift;

    // Use this for initialization
    void Start() {
        LevelManager.getInstance().addPolarizeable(this as IPolarizeable);
    }



    // Update is called once per frame
    void Update() {
        // Debug.Log(isAtTarget());

        //Make the path go to each waypoint target linearly, forever
        float step = speed * Time.deltaTime;
        stepInc += step;

        transform.localPosition = new Vector2(xamp * Mathf.Cos(xfreq * stepInc + xphase) + xshift, yamp * Mathf.Sin(yfreq * stepInc + yphase) + yshift);
    }

    public void onNotifyPolarize(int polarizeMode) {
        int filterMode = LevelManager.getInstance().getFilterMode();
        //Debug.Log(filterMode);
        //Kill all shadows
        //Remove player parenting
        Debug.Log(polarizeMode);
        Transform shadowPlatform = masterPlatform.Find("Shadow(Clone)");
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 1f);

        if (shadowPlatform != null) {
            if (shadowPlatform.Find("Player") != null) {
                shadowPlatform.Find("Player").parent = null;
            }
            Destroy(shadowPlatform.gameObject);
        }

        if (polarizeMode != 0) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 0.2f);

            GameObject newShadowPlatform = (GameObject)Instantiate(shadowPrefab, transform.position, Quaternion.identity);
            newShadowPlatform.GetComponent<ChildPlatform>().parentPlatform = transform;
            newShadowPlatform.transform.parent = masterPlatform;
            gameObject.GetComponent<PlatformEffector2D>().colliderMask = IGNORE_PLAYER_MASK_FLAG;
            newShadowPlatform.GetComponent<ChildPlatform>().type = polarizeMode;
        } else {
            gameObject.GetComponent<PlatformEffector2D>().colliderMask = -1;
        }
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
