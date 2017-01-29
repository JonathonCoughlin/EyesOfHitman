using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallOfFameSequence : MonoBehaviour {

    public GameObject FP47StartingLocation;
    public FirstPersonDrifter FP47Drifter;
    public FPClownController FP47Controller;
    public AudioSource m_BackgroundMusic;

    private Light[] m_Lights;
    private TableClown[] TableClowns;
    private SpeakingClown[] SpeakingClowns;
    public List<SplineWalker> Walkers;

    // Use this for initialization
    void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        m_BackgroundMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartHoFSequence()
    {
        TurnOnLights();
        StartWalkers();
       // AnimateClowns();
        ActivateFPControl();
        m_BackgroundMusic.Play();
    }

    public void TransitionSequence()
    {
        TurnOnLights();
        //AnimateClowns();
        StartWalkers();
        m_BackgroundMusic.Play();
    }

    public void EndSequence()
    {
        TurnOffLights();
        KillWalkers();
        KillClowns();
        m_BackgroundMusic.Stop();
    }

    public void KillWalkers()
    {
        if (Walkers != null)
        {
            Walkers.ForEach(delegate (SplineWalker curWalker)
            {
                Destroy(curWalker.gameObject);
            });
        }
        
    }

    
    public void StartWalkers()
    {
        Walkers.ForEach(delegate (SplineWalker curWalker)
        {
            curWalker.StartWalking();
        });
    }

    public void ActivateFPControl()
    {
        HitmanGameManager.KillAllCameras();
        FP47Drifter.transform.position = FP47StartingLocation.transform.position;
        FP47Drifter.transform.rotation = FP47StartingLocation.transform.rotation;
        FP47Controller.ActivateFPControl();
    }

    public void LowResourceSequence()
    {
        TurnOffLights();
        //PauseClowns();
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

    public void KillClowns()
    {
        TableClowns = GetComponentsInChildren<TableClown>();

        for (int ii = 0; ii < TableClowns.Length; ii++)
        {
            Destroy(TableClowns[ii].gameObject);
        }

        SpeakingClowns = GetComponentsInChildren<SpeakingClown>();

        for (int ii = 0; ii < SpeakingClowns.Length; ii++)
        {
            Destroy(SpeakingClowns[ii].gameObject);
        }
    }


}
