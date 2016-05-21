using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class ParentPlatformParametric : MonoBehaviour, IPolarizeable {
    private int IGNORE_PLAYER_MASK_FLAG = -257;

    public float speed;
    public Transform masterPlatform;
    public GameObject shadowPrefab;
    float stepInc = 0f;

	public float morphSpeed = 0.5f;
    public float xamp;
    public float xfreq = 1;
    public float xphase;
    public float xshift;
    public float yamp;
    public float yfreq = 1;
    public float yphase;
    public float yshift;

	private float[] initConditions;
	private float[] currentConditions;
	private float[] futureConditions;

    // Use this for initialization
    void Start() {
        LevelManager.getInstance().addPolarizeable(this as IPolarizeable);
		initConditions = new float[] {speed, xamp, xfreq, xphase, xshift, yamp, yfreq, yphase, yshift };
		currentConditions = (float[]) initConditions.Clone();
		futureConditions = (float[]) initConditions.Clone();
		//futureConditions [6] = 1f;
    }



    // Update is called once per frame
    void Update() {
        // Debug.Log(isAtTarget());

        //Make the path go to each waypoint target linearly, forever
		float step = currentConditions[0]* Time.deltaTime;
        stepInc += step;

		transform.localPosition = new Vector2(currentConditions[1]*Mathf.Cos(currentConditions[2] * stepInc + currentConditions[3]*Mathf.PI) + currentConditions[4],
			currentConditions[5] * Mathf.Sin(currentConditions[6] * stepInc + currentConditions[7]*Mathf.PI) + currentConditions[8]);


		//Morphing!
		//If you need to morph the path of a platform, keep it resonable, or else it gets real bad real fast :P
		//So try and keep the general shape of the path the same
		//Morph speed is how fast the morph is
		if (!Enumerable.SequenceEqual(currentConditions,futureConditions)) {
			
			for(int i = 0; i < currentConditions.Length; i++) {
				
				currentConditions [i] = Mathf.SmoothStep(currentConditions[i], futureConditions [i], Time.deltaTime*morphSpeed);
				if (Mathf.Abs(currentConditions [i] - futureConditions [i]) < 0.001f) {
					currentConditions [i] = futureConditions [i];
					//Debug.Log ("morphing complete");
				}
			}
			//Debug.Log("MEMEE");
		}
		//Debug.Log (currentConditions[1]);
		//Debug.Log (morphSpeed);
    }

	public void reset(){
		futureConditions = initConditions;
	}

	public void modify(float[] conditions){
		//Debug.Log ("AAAA");
		//yamp = Mathf.Lerp(yamp, 20f, 0.2f);
		//yamp = 9;
		//futr
		futureConditions = conditions;
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
}
