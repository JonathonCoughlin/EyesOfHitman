using UnityEngine;
using System.Collections;
using FirstPersonExploration;

[RequireComponent(typeof(AudioSource))]
public class BarkVolume : TriggerVolume {

    //Components
    private AudioSource m_Voice;

    //Parameters
    public AudioClip m_barkClip;

    //States
    private bool m_barking = false;

    // Use this for initialization
    void Start () {
        SetComponents();
	}

    protected override void SetComponents()
    {
        base.SetComponents();
        m_Voice = GetComponent<AudioSource>();
    }

    protected override void TriggerMe()
    {
        base.TriggerMe();
        if (!m_barking) StartCoroutine(Bark());
    }

    protected IEnumerator Bark()
    {
        m_barking = true;
        m_Voice.PlayOneShot(m_barkClip);
        yield return new WaitForSeconds(m_barkClip.length);
        m_barking = false;
    }
}
