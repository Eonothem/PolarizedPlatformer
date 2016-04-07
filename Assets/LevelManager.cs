using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    private static LevelManager instance;
    static int CURRENT_FILTER_MODE;

    enum POLARIZE_FILTER_MODE
    {
        NONE,
        HORIZONTAL,
        VERTICAL
    }
	// Use this for initialization
	void Start () {
        CURRENT_FILTER_MODE = (int)POLARIZE_FILTER_MODE.NONE;
	}

    public static LevelManager getInstance()
    {
        if (instance == null){
            instance = new LevelManager();
        }
        return instance;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
            setFilterMode((int)POLARIZE_FILTER_MODE.NONE);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            setFilterMode((int)POLARIZE_FILTER_MODE.HORIZONTAL);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            setFilterMode( (int)POLARIZE_FILTER_MODE.VERTICAL );
        }

        //Debug.Log(CURRENT_FILTER_MODE);
	}

    void setFilterMode(int filterMode)
    {
        CURRENT_FILTER_MODE = filterMode;
    }

    public int getFilterMode()
    {
        return CURRENT_FILTER_MODE;
    }

    
}
