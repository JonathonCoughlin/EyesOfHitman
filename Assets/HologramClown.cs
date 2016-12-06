using UnityEngine;
using System.Collections;


public enum HoloClownStates { closed, opening, speaking, paused };

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]

public class HologramClown : MonoBehaviour {

    //Components
    private AudioSource m_Voice;
    private Animator m_HologramAnimator;
    private Animator m_ClownAnimator;
    public GameObject m_HologramMachine;

    public HoloClownStates m_state;        

	// Use this for initialization
	void Start () {
        SetComponents();
        AnimateState();
	}

    private void SetComponents()
    {
        m_Voice = GetComponent<AudioSource>();
        m_ClownAnimator = GetComponent<Animator>();
        m_HologramAnimator = m_HologramMachine.GetComponent<Animator>();
    }

    private void AnimateState()
    {
        switch (m_state)
        {
            case HoloClownStates.closed:
                {
                    m_HologramAnimator.SetBool("Open", false);
                    m_HologramAnimator.SetBool("Running", false);
                    m_ClownAnimator.SetBool("MachineOn", false);
                    m_ClownAnimator.SetBool("Speaking", false);
                    break;
                }
            case HoloClownStates.opening:
                {
                    m_HologramAnimator.SetBool("Open", true);
                    m_HologramAnimator.SetBool("Running", false);
                    m_ClownAnimator.SetBool("MachineOn", false);
                    m_ClownAnimator.SetBool("Speaking", false);
                    break;
                }
            case HoloClownStates.speaking:
                {
                    m_HologramAnimator.SetBool("Open", true);
                    m_HologramAnimator.SetBool("Running", true);
                    m_ClownAnimator.SetBool("MachineOn", true);
                    m_ClownAnimator.SetBool("Speaking", true);
                    m_Voice.Play();
                    break;
                }
            case HoloClownStates.paused:
                {
                    m_HologramAnimator.SetBool("Open", true);
                    m_HologramAnimator.SetBool("Running", true);
                    m_ClownAnimator.SetBool("MachineOn", true);
                    m_ClownAnimator.SetBool("Speaking", false);
                    m_Voice.Pause();
                    break;
                }
        }
    }

	// Update is called once per frame
	void Update () {
	    if (m_state == HoloClownStates.opening)
        {
            //is opening animation finished?
            if (m_HologramAnimator.GetCurrentAnimatorStateInfo(0).IsName("Open")) { m_state = HoloClownStates.speaking; AnimateState(); }
        }
	}

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) { OpenMachine(); }
    }

    public void OpenMachine()
    {
        m_state = HoloClownStates.opening;
        AnimateState();
    }

    public void ClickMachine()
    {
        switch (m_state)
        {
            case HoloClownStates.closed: {
                    m_state = HoloClownStates.opening;
                    AnimateState();
                    break;
                }
            case HoloClownStates.paused:
                {
                    m_state = HoloClownStates.speaking;
                    AnimateState();
                    break;
                }
            case HoloClownStates.speaking:
                {
                    m_state = HoloClownStates.paused;
                    AnimateState();
                    break;
                }
        }
    }
}
