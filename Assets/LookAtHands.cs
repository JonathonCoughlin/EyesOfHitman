using UnityEngine;
using System.Collections;

public enum FPHandsOverrideType { LookAtHands, DisabledWaiter, Dab, AAA};

public class LookAtHands : MonoBehaviour {

    public FPHandsOverrideType m_handsLookType;

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
                switch (m_handsLookType)
                {
                    case FPHandsOverrideType.LookAtHands:
                        {
                            other.gameObject.GetComponent<FPClownController>().LookAtHands();
                            break;
                        }
                    case FPHandsOverrideType.DisabledWaiter:
                        {
                            other.gameObject.GetComponent<FPClownController>().DisabledWaiter();
                            break;
                        }
                    case FPHandsOverrideType.Dab:
                        {
                            other.gameObject.GetComponent<FPClownController>().Dab();
                            break;
                        }
                    case FPHandsOverrideType.AAA:
                        {
                            other.gameObject.GetComponent<FPClownController>().AAA();
                            break;
                        }

                }

                
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
