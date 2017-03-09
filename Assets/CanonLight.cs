using UnityEngine;
using System.Collections;

public class CanonLight : MonoBehaviour {

    public Light spotlight;
    private AudioSource m_LightSounds;

	// Use this for initialization
	void Start () {
        spotlight.enabled = false;
        m_LightSounds = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TurnOn ()
    {
        spotlight.enabled = true;
        m_LightSounds.Play();
    }

    public void TurnOff()
    {
        spotlight.enabled = false;
        m_LightSounds.Stop();
    }
}
