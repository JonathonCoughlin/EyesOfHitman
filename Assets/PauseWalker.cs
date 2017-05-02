using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SplineWalker))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class PauseWalker : MonoBehaviour {

    //Components
    private SplineWalker m_Walker;
    private Collider m_Collider;
    private Animator m_Animator;

    //States


	// Use this for initialization
	void Start () {
        SetComponents();
	}

    private void SetComponents()
    {
        m_Walker = GetComponent<SplineWalker>();
        m_Collider = GetComponent<Collider>();
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider enteredMe)
    {
        if (enteredMe.tag == "Player")
        {
            m_Walker.PauseWalking();
            Vector3 lookPos = enteredMe.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
            m_Animator.SetBool("OfferFood", true);
        }
    }

    void OnTriggerExit(Collider leftMe)
    {
        if (leftMe.tag == "Player")
        {
            m_Animator.SetBool("OfferFood", false);
            m_Walker.ResumeWalking();
        }
    }

}
