using UnityEngine;
using System.Collections;



public class FishingSequence : MonoBehaviour {

    //Components
    private Animator m_animator;
    public TutorialCanvas m_TutorialCanvas;
    
    //FishingSequence
    public SplineWalker fishingBoatWalker;
    public AudioSource FishingVoices;
    public Fisherman fisherman;
    public BoatDriver driver;

    public SplineWalker camWalker;
    public Camera descendingCamera;
    public SplineWalker cameraTarget;

    public TutorialCanvas tutorialUI;

    //47 Sequence
    public SplineWalker hitmanBoatWalker;
    public AudioSource titleMusic;
    public FirstPersonDrifter fp47;
    public Fishing47 fishing47;

    //Title
    public GameObject titleText;
    private SplineWalker titleWalker;

    private bool m_freeRoamStart = false;

    // Use this for initialization
	void Start () {
        SetComponents();
        
	}
	
    private void SetComponents()
    {
        titleWalker = titleText.GetComponent<SplineWalker>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
	void Update () {
	
	}

    public void FadeOut()
    {
        KillFP();
        titleMusic.Stop();
    }

    public void KillFP()
    {
        Destroy(fp47.gameObject);
    }

    public void StartFishingSequence(bool freeRoamStart)
    {
        m_freeRoamStart = freeRoamStart;
        m_animator.SetTrigger("BeginSequence");
    }

    private void BeginSequence()
    {
        HitmanGameManager.KillAllCameras();
        cameraTarget.StartWalking();
        camWalker.StartWalking();
        HitmanGameManager.ActivateCameraAndListen(descendingCamera); 
        if (m_freeRoamStart)
        {
            GoFirstPerson();
        }
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
        HitmanGameManager.KillAllCameras();
        fishing47.ActivateFPControl();
        m_TutorialCanvas.ShowClickKeys();
    }
}
