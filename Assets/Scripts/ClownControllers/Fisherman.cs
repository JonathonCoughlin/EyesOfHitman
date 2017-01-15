using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class Fisherman : MonoBehaviour {

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

    public void SitMe()
    {
        m_Animator.SetBool("Sitting", true);
    }

    public void FishMe()
    {
        m_Animator.SetBool("Sitting", false);
    }
}
