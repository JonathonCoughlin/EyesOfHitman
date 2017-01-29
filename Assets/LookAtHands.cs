using UnityEngine;
using System.Collections;

public class LookAtHands : MonoBehaviour {

    private Collider m_Collider;

    private int m_enterCount = 0;

	// Use this for initialization
	void Start () {
        m_Collider = GetComponent<Collider>();
	}
	
    void OnTriggerEnter(Collider other )
    {
        if (other.gameObject.tag == "Player")
        {
            m_enterCount++;
            if (m_enterCount == 1)
            {
                other.gameObject.GetComponent<FPClownController>().LookAtHands();
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
