﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class BalloonMachine : MonoBehaviour {

    private Animator m_Animator;

    public GameObject balloonPrefab;

    private FloatableBalloon m_Balloon;

    public int m_maxBalloons;
    private int m_totalBalloons;

	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
    }
    
    private void MakeBalloon()
    {
        if (m_totalBalloons < m_maxBalloons)
        {
            GameObject balloonObject = (GameObject)Instantiate(balloonPrefab);
            m_Balloon = balloonObject.GetComponent<FloatableBalloon>();
            m_Balloon.InitializeBalloon(this);
            m_totalBalloons++;
        }
        
    }

    public void InflateBalloon()
    {
        m_Balloon.Inflate();
    }

    public void PauseInflation()
    {
        m_Animator.speed = 0f;
        m_Balloon.PauseInflate();
    }

    public void ReleaseBalloon()
    {
        m_Balloon.FloatAway();
    }

    public void On()
    {
        m_Animator.SetBool("On", true);
        m_Animator.speed = 1f;
    }

    public void Off()
    {
        PauseInflation();
    }

    public void DeadBalloon()
    {
        m_totalBalloons--;
    }
}
