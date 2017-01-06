using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FirstPersonDrifter))]
public class FPClownController : MonoBehaviour {

    private FirstPersonDrifter m_Drifter;
    public Animator m_Animator;

    //Prop Holding Stuff
    public bool m_holdingProp { get; private set; }
    private GameObject m_prop;
    public GameObject handBone;

	// Use this for initialization
	void Start () {
        SetComponents();
        m_holdingProp = false;
	}
	
    private void SetComponents()
    {
        m_Drifter = GetComponent<FirstPersonDrifter>();
    }

	// Update is called once per frame
	void Update () {
        ManageWalkAnimation();
        if (m_holdingProp) ManageProp();
	}

    private void ManageWalkAnimation()
    {
        m_Animator.SetBool("Walking", m_Drifter.walking);
    }

    private void ManageProp()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_Animator.SetTrigger("AdjustHold");
        }
    }

    public void GrabProp(GameObject prop)
    {
        m_holdingProp = true;
        m_prop = prop;
        m_Animator.SetTrigger("Grab");
        if (prop.tag == "axe")
        {
            m_Animator.SetBool("HoldingObject", true);
            m_Animator.SetBool("HoldingAxe", true);
        }
        else
        {
            m_Animator.SetBool("HoldingObject", true);
            m_Animator.SetBool("HoldingAxe", false);
        }
    }

    private void ThrowProp()
    {

    }



}
