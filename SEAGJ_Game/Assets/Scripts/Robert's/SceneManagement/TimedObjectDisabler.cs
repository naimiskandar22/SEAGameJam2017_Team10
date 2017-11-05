using UnityEngine;
using System.Collections;

public class TimedObjectDisabler : MonoBehaviour {

    public float delayTimer;
    public bool activateOnEnable;
    
    void OnEnable()
    {
        if(activateOnEnable)
            Invoke("DisableDelay", delayTimer);
    }

	// Use this for initialization
	void Start () 
    {
        if(!activateOnEnable)
            Invoke("DisableDelay", delayTimer);
	}

    void DisableDelay()
    {
        gameObject.SetActive(false);
    }
}
