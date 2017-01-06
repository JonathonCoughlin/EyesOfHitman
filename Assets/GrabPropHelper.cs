using UnityEngine;
using System.Collections;
using JonClickSystem;

public class GrabPropHelper : MonoBehaviour {

    public HoldablePropClickEvents m_prop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PutPropInHand()
    {
        m_prop.GrabMe();
    }

    public void ThrowProp()
    {
        Transform camPos = Camera.main.transform;
        Ray throwRay = new Ray(camPos.position, camPos.forward);
        m_prop.ThrowMe(throwRay.direction);
    }

}
