using UnityEngine;
using System.Collections;

public enum WaiterOfferState { free, offering, recharging };

[RequireComponent(typeof(SplineWalker))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class PauseWalker : MonoBehaviour {

    //Components
    private SplineWalker m_Walker;
    private Collider m_Collider;
    private Animator m_Animator;

    //States
    public WaiterOfferState m_offerState { get; private set; }
    public float m_offerTimeLimit;
    private float m_offerStopWatch;
    public float m_rechargeTimeLimit;
    private PauseWalker m_otherWalker;
    private bool m_otherWalkerPresent = false;


	// Use this for initialization
	void Start () {
        SetComponents();
	}

    private void SetComponents()
    {
        m_Walker = GetComponent<SplineWalker>();
        m_Collider = GetComponent<Collider>();
        m_Animator = GetComponent<Animator>();

        m_offerState = WaiterOfferState.free;
    }

    void OnTriggerEnter(Collider enteredMe)
    {
        if (enteredMe.tag == "Waiter")
        {
            RegisterWalker(enteredMe.gameObject.GetComponent<PauseWalker>());
        }

        if (enteredMe.tag == "Player" && m_offerState == WaiterOfferState.free)
        {
            if (m_otherWalkerPresent && m_otherWalker.m_offerState != WaiterOfferState.offering)
            {
                OfferFood(enteredMe);
                m_otherWalker.RechargeMe();
            }
            else if (!m_otherWalkerPresent)
            {
                OfferFood(enteredMe);
            }
            
        }
    }

    private void RegisterWalker(PauseWalker toRegister)
    {
        m_otherWalker = toRegister;
        m_otherWalkerPresent = true;
        if (m_otherWalker.m_offerState == WaiterOfferState.offering)
        {
            RechargeMe();
        }
    }

    public void RechargeMe()
    {
        m_offerState = WaiterOfferState.recharging;
        StartCoroutine(Recharging(m_rechargeTimeLimit));
    }

    private IEnumerator Recharging(float time)
    {
        yield return new WaitForSeconds(time);
        m_offerState = WaiterOfferState.free;
    }

    void OnTriggerExit(Collider leftMe)
    {
        if (leftMe.tag == "Player")
        {
            FinishOffer();
        }

        if (leftMe.gameObject.GetComponent<PauseWalker>() != null)
        {
            m_otherWalkerPresent = false;
        }
    }

    private void FinishOffer()
    {
        RechargeMe();
        m_Animator.SetBool("OfferFood", false);
        m_Walker.ResumeWalking();
        m_offerState = WaiterOfferState.recharging;
    }

    private void OfferFood(Collider enteredMe)
    {
        m_Walker.PauseWalking();
        Vector3 lookPos = enteredMe.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
        m_Animator.SetBool("OfferFood", true);
        m_offerState = WaiterOfferState.offering;
    }

}
