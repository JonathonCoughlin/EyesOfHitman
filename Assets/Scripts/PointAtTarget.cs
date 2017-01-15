using UnityEngine;
using System.Collections;

public class PointAtTarget : MonoBehaviour {

    public GameObject m_target;

    public bool m_pointAtTarget = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (m_pointAtTarget)
        {
            transform.LookAt(m_target.transform);
        }
        
	}
}
