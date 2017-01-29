using UnityEngine;
using System.Collections;

public class FPControlOverride : MonoBehaviour {

    public FPClownController m_Controller;

	// Use this for initialization
	void Start () {
	
	}
	
    public void ReturnControl()
    {
        m_Controller.ActivateFPControl();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
