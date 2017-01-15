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

    //Title
    public GameObject titleText;
    private SplineWalker titleWalker;


    // Use this for initialization
	void Start () {
        SetComponents();
        KillAllCameras();
	}
	
    private void SetComponents()
    {
        titleWalker = titleText.GetComponent<SplineWalker>();
        m_animator = GetComponent<Animator>();
    }

    private void KillAllCameras()
    {
        Camera[] allCameras = FindObjectsOfType(typeof(Camera)) as Camera[];
        
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

    }
}
