using UnityEngine;
using System.Collections;

public class CanonLight : MonoBehaviour {

    public Light spotlight;

	// Use this for initialization
	void Start () {
        spotlight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TurnOn ()
    {
        spotlight.enabled = true;
    }

    public void TurnOff()
    {
        spotlight.enabled = false;
    }
}
