using UnityEngine;
using System.Collections;

public class ControlRoomSequence : MonoBehaviour {

    public GameObject FP47StartingLocation;
    public FirstPersonDrifter FP47Drifter;
    public FPClownController FP47Controller;

    public GameObject FoundWaiterCue;

    public CanonLight m_CanonLight;

    private Light[] m_Lights;
    private AudioSource m_BackgroundMusic;
    private AssassinationCues m_Cues;


	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_BackgroundMusic = GetComponent<AudioSource>();
        m_Cues = (AssassinationCues)FindObjectOfType(typeof(AssassinationCues));
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void LowResourceSequence()
    {
        TurnOffLights();
    }

    public void StartSequence()
    {
        TurnOnLights();
        //Disable Waiter Look
        m_Cues.WaiterFound();
        FoundWaiterCue.SetActive(false);
        m_Cues.CostumeOn();
        m_CanonLight.TurnOff();
        ActivateFPControl();
    }

    public void ActivateFPControl()
    {
        HitmanGameManager.KillAllCameras();
        FP47Drifter.transform.position = FP47StartingLocation.transform.position;
        FP47Drifter.transform.rotation = FP47StartingLocation.transform.rotation;
        FP47Controller.ActivateFPControl();
    }

    public void EndSequence()
    {
        TurnOffLights();
        m_BackgroundMusic.Stop();
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
}
