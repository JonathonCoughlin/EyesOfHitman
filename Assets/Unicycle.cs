using UnityEngine;
using System.Collections;

public class Unicycle : MonoBehaviour {

    private Animator m_Animator;

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
	
	}

    public void CycleOn()
    {
        m_Animator.SetBool("Cycling", true);
    }

    public void CycleOff()
    {
        m_Animator.SetBool("Cycling", false);
    }

}
