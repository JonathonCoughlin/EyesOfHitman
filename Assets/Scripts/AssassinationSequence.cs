using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AssassinationSequence : MonoBehaviour {

    //Parameters
    public float m_slowMoTimeScale;

    //Components
    private Animator m_Animator;
    private AudioSource m_SongPlayer;

    //Characters
    public LippyPerformance m_Lippy;
    public Agent47Performance m_47;
    public FirstPersonDrifter m_FP47;
    public TableClown m_LippyMingling;
    public GameObject m_LandingTarget;
    public GameObject m_OldBoat;
    public GameObject m_NewBoat;
    public SplineWalker m_GetAwayWalker;
    public SplineWalker m_GetAwayCamWalker;
    public GameObject m_Masks;
    public GameObject m_MasksOnFloor;
    public GameObject m_Wilboe;
    public SplineWalker m_CreditsTarget;


    //Cameras
    public Camera EntranceCam;
    public Camera ControlRoomCam;
    public Camera CableCam;
    public Camera CycleCam;
    public Camera BallroomCam;
    public Camera HardCutCam;
    public Camera HoFCam;
    public Camera RedCarpetCam;
    public Camera GetawayCam;


    public AudioSource m_Piano;
    public AudioSource m_Speaker1;
    public AudioSource m_Speaker2;
    public AudioSource m_Mumblers;



   // Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
        m_SongPlayer = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void Animate()
    {
        //kill Music
        m_Piano.Stop();
        m_Speaker1.Stop();
        m_Speaker2.Stop();
        m_Mumblers.Stop();

        //Switch 47 and Lippy
        Destroy(m_LippyMingling.gameObject);
        m_Lippy.gameObject.SetActive(true);
        m_47.gameObject.SetActive(true);

        //Move FP47
        m_OldBoat.SetActive(false);
        m_NewBoat.SetActive(true);
        m_FP47.MakeMeAChildOfYourPeace(m_LandingTarget);
        m_FP47.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.NoControl);

        m_Animator.SetTrigger("Animate");
    }

    public void GoEntranceCam()
    {
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        HitmanGameManager.ActivateCameraAndListen(EntranceCam);
        //Move Camera
        EntranceCam.GetComponent<SplineWalker>().StartWalking();
        
    }

    public void DanceLippy()
    {
        m_Lippy.BeginDance();
    }

    public void PlaySong()
    {
        //Start Music
        m_SongPlayer.Play();
    }
    
    public void GoBallroomCam()
    {
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        HitmanGameManager.ActivateCameraAndListen(BallroomCam);
        //MoveCamera
        BallroomCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoHardCutCam()
    {
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        HitmanGameManager.ActivateCameraAndListen(HardCutCam);
        //MoveCamera
        HardCutCam.GetComponent<Animator>().SetTrigger("AnimateMe");
    }

    public void GoControlRoomCam()
    {
        MakeLippyDead();
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        HitmanGameManager.ActivateCameraAndListen(ControlRoomCam);
        //Move Camera
        ControlRoomCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoGetawayCam()
    {
        m_GetAwayWalker.StartWalking();
        m_GetAwayCamWalker.StartWalking();
        m_CreditsTarget.StartWalking();
        HitmanGameManager.KillAllCameras();
        HitmanGameManager.ActivateCameraAndListen(GetawayCam);
        m_Wilboe.gameObject.SetActive(true);
        
    }

    public void MakeLippyDead()
    {
        m_Lippy.MakeLippyDead();
        m_Masks.transform.position = m_MasksOnFloor.transform.position;
        m_Masks.transform.rotation = m_MasksOnFloor.transform.rotation;
    }

    public void GoRedCarpetCam()
    {
        HitmanGameManager.KillAllCameras();
        HitmanGameManager.ActivateCameraAndListen(RedCarpetCam);
        RedCarpetCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoHoFCam()
    {
        HitmanGameManager.KillAllCameras();
        HitmanGameManager.ActivateCameraAndListen(HoFCam);
        HoFCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoCableCam()
    {
        HitmanGameManager.KillAllCameras();
        HitmanGameManager.ActivateCameraAndListen(CableCam);
        CableCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoCycleCam()
    {
        HitmanGameManager.KillAllCameras();
        HitmanGameManager.ActivateCameraAndListen(CycleCam);
        CycleCam.enabled = true;
    }

    public void SlowTimeScale()
    {
        Time.timeScale = m_slowMoTimeScale;
    }

    public void Animate47AxeSwing()
    {
        m_47.SwingAxe();
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }


}
