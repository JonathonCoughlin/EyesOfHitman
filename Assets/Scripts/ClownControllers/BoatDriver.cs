using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
public class BoatDriver : MonoBehaviour {

    //Animator
    private Animator m_Animator;

    //Audio
    public AudioSource m_Motor;
    public AudioClip m_motorStartClip;
    public AudioClip m_motorIdleClip;
    public AudioClip m_motorRunClip;


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

    public void StartMotor()
    {
        m_Animator.SetTrigger("StartMotor");
        m_Animator.SetBool("MotorOn", true);
    }

    public void PullMotor()
    {
        m_Motor.PlayOneShot(m_motorStartClip);
        m_Motor.loop = false;
    }

    public void MotorIdle()
    {
        m_Motor.clip = m_motorIdleClip;
        m_Motor.loop = true;
        m_Motor.Play();
    }

    
}
