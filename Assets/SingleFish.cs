using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SingleFish : MonoBehaviour {

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

    public void FishermanAction()
    {
        m_Animator.SetTrigger("FishermanAction");
    }

    public void FlopMe()
    {
        m_Animator.SetInteger("PanicType", 0);
        m_Animator.SetBool("Panicking", true);
    }

    public void WobbleMe()
    {
        m_Animator.SetInteger("PanicType", 1);
        m_Animator.SetBool("Panicking", true);
    }

    public void CalmMe()
    {
        m_Animator.SetBool("Panicking", false);
    }




}
