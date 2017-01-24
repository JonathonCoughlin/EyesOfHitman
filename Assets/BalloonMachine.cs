using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class BalloonMachine : MonoBehaviour {

    private Animator m_Animator;

    public GameObject balloonPrefab;

    private FloatableBalloon m_Balloon;

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

    private void MakeBalloon()
    {
        GameObject balloonObject = (GameObject)Instantiate(balloonPrefab);
        m_Balloon = balloonObject.GetComponent<FloatableBalloon>();
        m_Balloon.InitializeBalloon(this);
    }

    public void InflateBalloon()
    {
        m_Balloon.Inflate();
    }

    public void PauseInflation()
    {
        m_Balloon.PauseInflate();
    }

    public void ReleaseBalloon()
    {

    }

    public void On()
    {
        m_Animator.SetBool("On", true);
    }

    public void Off()
    {
        m_Animator.SetBool("On", false);
        PauseInflation();
    }
}
