using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class FlashGroup : MonoBehaviour {

    public List<FlashCamera> m_cameras;
    private Collider m_Collider;

	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Collider = GetComponent<Collider>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider hitMe)
    {
        m_cameras.ForEach(delegate (FlashCamera cam)
        {
            cam.FlashMe();
        });
    }
}
