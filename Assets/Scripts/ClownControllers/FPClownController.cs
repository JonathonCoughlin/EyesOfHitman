using UnityEngine;
using System.Collections;
using JonClickSystem;

[RequireComponent(typeof(FirstPersonDrifter))]
public class FPClownController : MonoBehaviour {

    private FirstPersonDrifter m_Drifter;
    public Animator m_Animator;

    //Prop Holding Stuff
    public bool m_holdingProp { get; private set; }
    private HoldablePropClickEvents m_prop;
    public GameObject handBone;
    public GrabPropHelper m_PropHelper;

    //Player Control
    private bool m_playerControl = false;

	// Use this for initialization
	void Start () {
        SetComponents();
        m_holdingProp = false;
	}
	
    private void SetComponents()
    {
        m_Drifter = GetComponent<FirstPersonDrifter>();
    }

    public void ActivateFPControl()
    {
        m_Drifter.SetMainCamera();
        m_Drifter.SwitchControlTypes(WalkControlLimits.FullControl, LookControlLimits.FullControl);
        m_playerControl = true;
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
            if (m_prop.m_throwable)
            {
                if (m_prop.m_held)
                {
                    m_Animator.SetTrigger("Throw");
                }
                

            } else
            {
                m_Animator.SetTrigger("AdjustHold");
            }
        }
    }

    public void GrabProp(GameObject prop)
    {
        m_holdingProp = true;
        m_prop = prop.GetComponent<HoldablePropClickEvents>();
        m_PropHelper.m_prop = prop.GetComponent<HoldablePropClickEvents>();
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
