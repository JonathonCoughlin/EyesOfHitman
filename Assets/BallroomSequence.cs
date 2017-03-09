using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallroomSequence : MonoBehaviour {

    public GameObject FP47StartingLocation;
    public FirstPersonDrifter FP47Drifter;
    public FPClownController FP47Controller;

    public AudioSource Piano;
    private Light[] m_Lights;
    private TableClown[] TableClowns;
    private SpeakingClown[] SpeakingClowns;
    public List<SplineWalker> Waiters;
    public CanonLight m_CanonLight;

    public AudioTrigger m_Mumbler;


    // Use this for initialization
    void Start()
    {

    }


    public void StartBallroomSequence()
    {
        TurnOnLights();
        AnimateClowns();
        PlayPiano();
        StartWaiters();
        ActivateFPControl();
        m_CanonLight.TurnOff();
        m_Mumbler.Reset();
    }

    public void TransitionSequence()
    {
        TurnOnLights();
        AnimateClowns();
        PlayPiano();
        StartWaiters();
        m_CanonLight.TurnOff();
        m_Mumbler.Reset();
    }

    public void LowResourceSequence()
    {
        TurnOffLights();
        PauseClowns();
        Piano.Pause();
        m_Mumbler.Reset();        
    }

    public void TurnOnLights()
    {
        m_Lights = GetComponentsInChildren<Light>();

        for (int ii = 0; ii < m_Lights.Length; ii++)
        {
            m_Lights[ii].enabled = true;
        }
    }

    public void TurnOffLights()
    {
        m_Lights = GetComponentsInChildren<Light>();

        for (int ii = 0; ii < m_Lights.Length; ii++)
        {
            m_Lights[ii].enabled = false;
        }
    }

    public void AnimateClowns()
    {
        TableClowns = GetComponentsInChildren<TableClown>();

        for (int ii = 0; ii < TableClowns.Length; ii++)
        {
            TableClowns[ii].gameObject.GetComponent<Animator>().enabled = true;
            TableClowns[ii].gameObject.GetComponent<ClownShuffler>().enabled = true;
            TableClowns[ii].gameObject.GetComponent<ClownShuffler>().ShuffleMe();
            TableClowns[ii].RandAnimationStartPos();
        }

        SpeakingClowns = GetComponentsInChildren<SpeakingClown>();

        for (int ii = 0; ii < SpeakingClowns.Length; ii++)
        {
            SpeakingClowns[ii].gameObject.GetComponent<Animator>().enabled = true;
            SpeakingClowns[ii].gameObject.GetComponent<ClownShuffler>().enabled = true;
            SpeakingClowns[ii].gameObject.GetComponent<ClownShuffler>().ShuffleMe();
        }
    }

    public void PauseClowns()
    {
        TableClowns = GetComponentsInChildren<TableClown>();

        for (int ii = 0; ii < TableClowns.Length; ii++)
        {
            TableClowns[ii].gameObject.GetComponent<Animator>().enabled = false;
        }

        SpeakingClowns = GetComponentsInChildren<SpeakingClown>();

        for (int ii = 0; ii < SpeakingClowns.Length; ii++)
        {
            SpeakingClowns[ii].gameObject.GetComponent<Animator>().enabled = false;
        }
    }

    public void PlayPiano()
    {
        Piano.Play();
        Piano.loop = true;
    }

    public void StartWaiters()
    {
        Waiters.ForEach(delegate (SplineWalker curWaiter)
        {
            curWaiter.StartWalking();
        });
    }

    public void ActivateFPControl()
    {
        HitmanGameManager.KillAllCameras();
        FP47Drifter.transform.position = FP47StartingLocation.transform.position;
        FP47Drifter.transform.rotation = FP47StartingLocation.transform.rotation;
        FP47Controller.ActivateFPControl();
    }

}
