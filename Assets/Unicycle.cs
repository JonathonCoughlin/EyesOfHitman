﻿using UnityEngine;
using System.Collections;

public class Unicycle : MonoBehaviour {

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

    public void SetCycleAnimation(bool animateCycle)
    {
        m_Animator.SetBool("Cycling", animateCycle);
    }
}
