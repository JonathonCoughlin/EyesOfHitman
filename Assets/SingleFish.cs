using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class SingleFish : MonoBehaviour {

    private Animator m_Animator;

    public List<GameObject> m_Credits;
    public int m_creditsIdx;
   
    // Use this for initialization
	void Start () {
        SetComponents();
        UpdateCredits();
	}
	
    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    private void UpdateCredits()
    {
        m_Credits.ForEach(delegate (GameObject creditLine)
        {
            creditLine.SetActive(false);
        });

        m_Credits[m_creditsIdx].SetActive(true);
    }

    public void NextCredit()
    {
        m_creditsIdx++;
        if (m_creditsIdx >= m_Credits.Count) m_creditsIdx = 0;
        UpdateCredits();
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
