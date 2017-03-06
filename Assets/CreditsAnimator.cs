using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class CreditsAnimator : MonoBehaviour {

    private Animator m_Animator;

    // Use this for initialization
    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AnimateMe()
    {
        m_Animator.SetTrigger("AnimateMe");
    }
}
