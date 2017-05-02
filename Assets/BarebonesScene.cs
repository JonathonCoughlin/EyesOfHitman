using UnityEngine;
using System.Collections;

public class BarebonesScene : MonoBehaviour {

    public FPClownController m_Player;
    private bool activated = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (!activated)
        {
            m_Player.ActivateFPControl();
            activated = true;
        }
	}
}
