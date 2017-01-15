using UnityEngine;
using System.Collections;

public class IntroSceneManager : MonoBehaviour {
    
    //TitleScreen
    private AudioSource m_TitleAudio;
    private Animator m_TitleAnimator;

    //Intro Cinematic
    public IntroCinematicController m_IntroCinematicController;

    //States
    private bool m_ReadyForClick = false;


	// Use this for initialization
	void Start () {
        SetComponents();	
	}
	
    private void SetComponents()
    {
        m_TitleAudio = GetComponent<AudioSource>();
        m_TitleAnimator = GetComponent<Animator>();
        GetComponent<Canvas>().enabled = true;
    }

	// Update is called once per frame
	void Update () {
	    if (m_ReadyForClick && Input.GetKeyDown(KeyCode.Mouse0))
        {
            BeginGame();
        }
	}

    public void ReadyForClick()
    {
        m_ReadyForClick = true;
    }

    private void BeginGame()
    {
        m_TitleAnimator.SetTrigger("BeginGame");
        m_ReadyForClick = false;
    }

    public void TransitionToIntroCinematic()
    {
        m_TitleAudio.Stop();
        m_IntroCinematicController.BeginIntroCinematic();
    }
    
}
