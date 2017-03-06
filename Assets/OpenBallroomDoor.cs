using UnityEngine;
using System.Collections;

public class OpenBallroomDoor : MonoBehaviour {

    private AssassinationCues m_Cues;
    private Collider m_Collider;
    public FPClownController m_Player;
    private Animator m_Animator;

    //States
    private bool m_cueTriggered = false;

	// Use this for initialization
	void Start () {
        m_Cues = (AssassinationCues)FindObjectOfType(typeof(AssassinationCues));
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !m_cueTriggered)
        {
            m_Cues.WaiterFound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Hopefully aligns with audio... we'll see
        if (other.gameObject.tag == "Player" && !m_cueTriggered)
        {
            m_Animator.SetTrigger("BeginLook");
            m_cueTriggered = true;
        }
    }

    public void FocusOnWaiter()
    {
        m_Player.LookAtWaiter();
    }
    public void StopTracking()
    {
        m_Player.StopTracking();
    }
}
