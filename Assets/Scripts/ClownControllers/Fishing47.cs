using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Fishing47 : MonoBehaviour {

    //Components
    private Animator m_Animator;
    public FirstPersonDrifter m_Drifter;
    public SingleFish m_Fish;
    public SplineWalker m_BoatWalker;
    public GameObject m_FishingPole;

    //States
    private bool m_playerControl;
    public enum FishingMachine { Idle, Fishing, DoneFishing}
    public FishingMachine m_FishingState;

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
        if (m_playerControl)
        {
            switch (m_FishingState)
            {
                case FishingMachine.Idle:
                    {
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            m_Animator.SetTrigger("FishingAction");
                            m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.VerticalOnly);
                            m_FishingState = FishingMachine.Fishing;
                            m_Fish.FishermanAction();
                        }

                        if (m_BoatWalker.SplinePos() >= 1f)
                        {
                            QuitFishing();
                        }
                        break;
                        
                    }
                case FishingMachine.Fishing:
                    {
                        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
                        {
                            m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.FullControl);
                            m_FishingState = FishingMachine.Idle;
                        }
                        break;
                    }
            }


        }
    }

    public void QuitFishing()
    {
        m_FishingState = FishingMachine.DoneFishing;
        DropPoleAndFish();
        ActivateFPWalk();
    }

    public void ActivateFPControl()
    {
        m_Drifter.SetMainCamera();
        m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.FullControl);
        m_Animator.SetBool("PlayerControl",true);
        m_playerControl = true;
    }

    public void ActivateFPWalk()
    {
        m_Drifter.SwitchControlTypes(WalkControlLimits.FullControl, LookControlLimits.FullControl);
        m_Animator.SetTrigger("DoneFishing");
    }

    public void DropPoleAndFish()
    {
        Destroy(m_FishingPole);
        Destroy(m_Fish.gameObject);
    }

}
