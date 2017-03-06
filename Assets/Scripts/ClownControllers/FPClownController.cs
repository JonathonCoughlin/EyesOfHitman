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

    //Unicycle Stuff
    public bool m_onUnicycle;
    public Unicycle m_Unicycle;
    public HeadBob m_HeadBob;
    public GameObject m_Cheese;

    //Player Control
    private bool m_playerControl = false;

    //Tracking Helpers
    public GameObject m_waiterTrackTarget;

	// Use this for initialization
	void Start () {
        SetComponents();
        m_holdingProp = false;
        ManageUnicycleVisibility();
	}
	
    private void SetComponents()
    {
        m_Drifter = GetComponent<FirstPersonDrifter>();
    }

    private void ManageUnicycleVisibility()
    {
        m_Animator.SetBool("Unicycle", m_onUnicycle);
        m_Unicycle.gameObject.SetActive(m_onUnicycle);
        if (m_onUnicycle)
        {
            m_HeadBob.midpoint = 1.45f;
        } else
        {
            m_HeadBob.midpoint = 1.15f;
        }
    }

    public void ActivateUnicycle()
    {
        m_onUnicycle = true;
        ManageUnicycleVisibility();
        m_Cheese.SetActive(true);
        m_HeadBob.enabled = false;
    }

    public void DeactivateUnicycle()
    {
        m_onUnicycle = false;
        m_Cheese.SetActive(false);
        ManageUnicycleVisibility();
        m_HeadBob.enabled = true;
    }

    public void LookAtHands()
    {
        m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.NoControl);
        m_playerControl = false;
        m_Animator.SetTrigger("LookAtHands");
    }

    public void DisabledWaiter()
    {
        m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.NoControl);
        m_playerControl = false;
        m_Animator.SetTrigger("DisabledWaiter");
    }

    public void Dab()
    {
        m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.NoControl);
        m_playerControl = false;
        m_Animator.SetTrigger("Dab");
    }

    public void AAA()
    {
        m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.NoControl);
        m_playerControl = false;
        m_Animator.SetTrigger("AAA");
    }

    public void LookAtWaiter()
    {
        m_Drifter.TrackTarget(m_waiterTrackTarget, WalkControlLimits.FullControl);
        m_playerControl = true;
        //m_Animator.SetTrigger(); // placeholder for FPHand animation
    }

    public void StopTracking()
    {
        m_Drifter.SwitchControlTypes(WalkControlLimits.FullControl, LookControlLimits.FullControl);
        m_playerControl = true;
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
        if (m_onUnicycle)
        {
            m_Unicycle.GetComponent<Animator>().SetBool("Cycling", m_Drifter.walking);
        }        
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
