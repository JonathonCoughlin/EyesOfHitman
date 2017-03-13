﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayRodkinRemo : MonoBehaviour {

    private AudioSource m_Voice;
    public AudioClip m_dorksClip;

	// Use this for initialization
	void Start () {
        m_Voice = GetComponent<AudioSource>();
	}
	
    public void PlayDorks()
    {
        m_Voice.PlayOneShot(m_dorksClip);
    }

}
