using UnityEngine;
using System.Collections;

public class WaiterController : MonoBehaviour {

    public Unicycle m_Cycle;
    private Animator m_Animator;

    private bool m_cycling = false;

	// Use this for initialization
	void Start () {
        SetComponents();
    }

    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
	    if (!m_cycling)
        {
            CycleOn();
        }
	}

    private void CycleOn()
    {
        m_Animator.SetBool("Cycling",true);
        m_Cycle.CycleOn();
    }
}
