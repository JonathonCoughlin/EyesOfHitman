using UnityEngine;
using System.Collections;
using FirstPersonExploration;

[RequireComponent(typeof(Collider))]
public class ReceiveSharps : MonoBehaviour {

    protected Collider m_Collider;

	// Use this for initialization
	void Start () {
        SetComponents();
	}

    protected void SetComponents()
    {
        m_Collider = GetComponent<Collider>();
    }
	
    void OnTriggerEnter (Collider hitMe)
    {
        StickMeWith(hitMe);
    }

    void OnCollisionEnter (Collision collision)
    {
        Collider hitMe = collision.collider;
        StickMeWith(hitMe);
    }

    protected void StickMeWith(Collider hitMe)
    {
        if (hitMe.tag == "sharp")
        {
            hitMe.GetComponent<Prop>().StickMe();
        }
    }

}
