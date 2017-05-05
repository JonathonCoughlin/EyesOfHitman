using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class Fishing47 : MonoBehaviour {

    //Components
    private Animator m_Animator;
    public FirstPersonDrifter m_Drifter;
    public List<SingleFish> m_Fish = new List<SingleFish>();
    private int m_fishListIDX = 0;
    public GameObject m_Hook;
    public GameObject m_FishHoldingHand;
    private SingleFish m_fishInstance;
    public SplineWalker m_BoatWalker;
    public GameObject m_FishingPole;
    public TutorialCanvas m_TutorialCanvas;

    //States
    private bool m_playerControl;
    public enum FishingMachine { Idle, Fishing, DoneFishing}
    public enum FishermanStatus { NoFish, Catching}
    public FishingMachine m_FishingState;
    public FishermanStatus m_FishermanStatus;

	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
        m_FishermanStatus = FishermanStatus.NoFish;
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
                            switch (m_FishermanStatus)
                            {
                                case FishermanStatus.NoFish:
                                    {
                                        SpawnFish();
                                        m_FishermanStatus = FishermanStatus.Catching;                                        
                                        break;
                                    }
                                case FishermanStatus.Catching:
                                    {
                                        m_FishermanStatus = FishermanStatus.NoFish;
                                        break;
                                    }
                                
                            }                            
                            m_fishInstance.FishermanAction();
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

    public void SpawnFish()
    {
        SingleFish spawnMe = m_Fish[m_fishListIDX];
        m_fishInstance = (SingleFish)Instantiate(spawnMe);
        m_fishInstance.InitializeMe(m_Hook,m_FishHoldingHand);
        m_fishListIDX++;
        if (m_fishListIDX >= m_Fish.Count) m_fishListIDX = 0;
    }

    public void QuitFishing()
    {
        m_FishingState = FishingMachine.DoneFishing;
        DropPoleAndFish();
        ActivateFPWalk();
        m_TutorialCanvas.ShowWalkKeys();
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
        Destroy(m_fishInstance.gameObject);
    }

}
