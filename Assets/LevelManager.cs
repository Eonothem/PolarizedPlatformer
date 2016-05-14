using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    private static LevelManager instance;
    private int CURRENT_FILTER_MODE;
    private List<IPolarizeable> polarizeables = new List<IPolarizeable>();

    enum POLARIZE_FILTER_MODE
    {
        NONE,
        HORIZONTAL,
        VERTICAL
    }

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	// Use this for initialization
	void Start () {
        CURRENT_FILTER_MODE = (int)POLARIZE_FILTER_MODE.NONE;
	}

    public static LevelManager getInstance()
    {
        return instance;
    }

    public void addPolarizeable(IPolarizeable p)
    {
       // p.onNotifyPolarize();
        polarizeables.Add(p);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
            setFilterMode((int)POLARIZE_FILTER_MODE.NONE);
            //Debug.Log("AAAA");
            notifyPolarizeables(0);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            setFilterMode((int)POLARIZE_FILTER_MODE.HORIZONTAL);
           // notifyPlatforms();
            notifyPolarizeables(1);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            setFilterMode( (int)POLARIZE_FILTER_MODE.VERTICAL );
            //notifyPlatforms();
            notifyPolarizeables(2);
        }

        //Debug.Log(CURRENT_FILTER_MODE);
	}

    public void notifyPolarizeables(int polarizeMode)
    {
        foreach(IPolarizeable i in polarizeables){
            i.onNotifyPolarize(polarizeMode);
        }
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
