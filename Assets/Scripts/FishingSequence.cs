using UnityEngine;
using System.Collections;



public class FishingSequence : MonoBehaviour {

    //Components
    private Animator m_animator;
    
    //FishingSequence
    public SplineWalker fishingBoatWalker;
    public AudioSource FishingVoices;
    public Fisherman fisherman;
    public BoatDriver driver;

    public SplineWalker camWalker;
    public Camera descendingCamera;
    public SplineWalker cameraTarget;

    //47 Sequence
    public SplineWalker hitmanBoatWalker;
    public AudioSource titleMusic;
    public FirstPersonDrifter fp47;
    public Fishing47 fishing47;

    //Title
    public GameObject titleText;
    private SplineWalker titleWalker;


    // Use this for initialization
	void Start () {
        SetComponents();
        
	}
	
    private void SetComponents()
    {
        titleWalker = titleText.GetComponent<SplineWalker>();
        m_animator = GetComponent<Animator>();
    }

    private void KillAllCameras()
    {
        Camera[] allCameras = FindObjectsOfType(typeof(Camera)) as Camera[];
        for (int ii = 0; ii < allCameras.Length; ii++)
        {
            allCameras[ii].enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void StartFishingSequence()
    {
        m_animator.SetTrigger("BeginSequence");
    }

    private void BeginSequence()
    {
        KillAllCameras();
        cameraTarget.StartWalking();
        camWalker.StartWalking();
        descendingCamera.enabled = true;
    }

    public void StartTalking()
    {
        FishingVoices.Play();
    }

    public void HitmanBoatCue()
    {
        hitmanBoatWalker.StartWalking();
    }

    public void PlayTitleMusic()
    {
        titleMusic.Play();
    }

    public void MoveTitleText()
    {
        titleWalker.StartWalking();
    }

    public void SitFisherman()
    {
        fisherman.SitMe();
    }

    public void StartMotor()
    {
        driver.StartMotor();
    }

    public void CueMotorNoise()
    {
        driver.PullMotor();
    }

    public void CueMotorIdle()
    {
        driver.MotorIdle();
    }

    public void GoFirstPerson()
    {
        KillAllCameras();
        fishing47.ActivateFPControl();

    }
}
