using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum FlashType { single, multiple};
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(FlashCamera))]
public class FlashGroup : MonoBehaviour {

    public List<FlashCamera> m_cameras;
    private Collider m_Collider;

    private FlashCamera m_SingleFlash;
    public FlashType m_FlashType;

	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Collider = GetComponent<Collider>();
        m_SingleFlash = GetComponent<FlashCamera>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider hitMe)
    {
        switch (m_FlashType)
        {
            case FlashType.single:
                {
                    m_SingleFlash.FlashMe();
                    break;
                }
            case FlashType.multiple:
                {
                    m_cameras.ForEach(delegate (FlashCamera cam)
                    {
                        cam.FlashMe();
                    });
                    break;
                }
        }

        
    }
}
