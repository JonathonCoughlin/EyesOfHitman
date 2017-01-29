using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RedCarpetSequence : MonoBehaviour {

    public GameObject FP47StartingLocation;
    public FirstPersonDrifter FP47Drifter;
    public FPClownController FP47Controller;
    private AudioSource m_BackgroundMusic;
    public TableClown LippyStatic;
    public FlashCamera FlickerCam1;
    public FlashCamera FlickerCam2;
    public AudioSource Mumbler;

    //Things to turn off
    public List<SplineWalker> m_Cars;
    public List<Light> m_Lights;

    // Use this for initialization
    void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_BackgroundMusic = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
        
	}

    public void StartRedCarpetSequence()
    {
        TurnOnLights();
        ActivateCars();
        ActivateFPControl();
        m_BackgroundMusic.Play();
    }

    public void PauseRedCarpetSequence()
    {
        TurnOffLights();

    }

    public void EndRedCarpetSequence()
    {
        TurnOffLights();
        KillCars();
        m_BackgroundMusic.Stop();
        Destroy(LippyStatic.gameObject);
        Destroy(FlickerCam1.gameObject);
        Destroy(FlickerCam2.gameObject);
        Destroy(Mumbler.gameObject);
    }

    public void ActivateFPControl()
    {
        HitmanGameManager.KillAllCameras();
        FP47Drifter.transform.position = FP47StartingLocation.transform.position;
        FP47Drifter.transform.rotation = FP47StartingLocation.transform.rotation;
        FP47Controller.ActivateFPControl();
    }

    public void ActivateCars()
    {
        m_Cars.ForEach(delegate (SplineWalker curCar)
        {
            curCar.StartWalking();
        });
    }

    public void KillCars()
    {
        m_Cars.ForEach(delegate (SplineWalker curCar)
        {
            Destroy(curCar.gameObject);
        });
    }

    public void TurnOnLights()
    {
        m_Lights.ForEach(delegate (Light curLight)
        {
            curLight.enabled = true; 
        });
        
    }

    public void TurnOffLights()
    {
        m_Lights.ForEach(delegate (Light curLight)
        {
            curLight.enabled = false;
        });
    }

    

}
