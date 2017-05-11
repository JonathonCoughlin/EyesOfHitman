using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RedCarpetSequence : MonoBehaviour {

    public GameObject FP47StartingLocation;
    public FirstPersonDrifter FP47Drifter;
    public FPClownController FP47Controller;
    private AudioSource m_BackgroundMusic;
    public TableClown LippyStatic;
    
    public AudioSource Mumbler;
    public TutorialCanvas m_TutorialCanvas;

    //Things to turn off
    public List<SplineWalker> m_Cars;
    public List<Light> m_Lights;
    public List<GameObject> m_Photographers;

    // Use this for initialization
    void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_BackgroundMusic = GetComponent<AudioSource>();
    }

    public void StartRedCarpetSequence()
    {
        TurnOnLights();
        ActivateCars();
        ActivateFPControl();
        m_BackgroundMusic.Play();
        m_TutorialCanvas.ShowDanceKeys();
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
        Destroy(Mumbler.gameObject);
        KillPhotographers();
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

    public void KillPhotographers()
    {
        m_Photographers.ForEach(delegate (GameObject curGuy)
        {
            Destroy(curGuy.gameObject);
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
