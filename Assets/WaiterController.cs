using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SplineWalker))]
public class WaiterController : MonoBehaviour {

    public Unicycle m_Cycle;
    private Animator m_Animator;
    private SplineWalker m_SplineWalker;

    private bool m_cycling = false;

	// Use this for initialization
	void Start () {
        SetComponents();
    }

    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
        m_SplineWalker = GetComponent<SplineWalker>();
    }

    // Update is called once per frame
    void Update () {
        bool walkerPaused = m_SplineWalker.m_paused;
	    if (walkerPaused == m_cycling)
        {
            ManageCycleAnimation(!walkerPaused);
        } 
	}

    private void ManageCycleAnimation(bool tempCue)
    {
        m_cycling = tempCue;
        m_Animator.SetBool("Cycling",m_cycling);
        m_Cycle.SetCycleAnimation(m_cycling);
    }
}
