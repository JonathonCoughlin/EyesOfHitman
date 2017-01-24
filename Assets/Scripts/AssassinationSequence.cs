using UnityEngine;
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
    
    //Cameras
    public Camera EntranceCam;
    public Camera ControlRoomCam;
    public Camera CableCam;
    public Camera CycleCam;
    public Camera BallroomCam;
    public Camera HoFCam;
    public Camera RedCarpetCam;
    public Camera DotGobblerCam;


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
        m_Animator.SetTrigger("Animate");
    }

    public void GoEntranceCam()
    {
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        EntranceCam.enabled = true;
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

    //public Camera EntranceCam;
    //public Camera ControlRoomCam;
    //public Camera CableCam;
    //public Camera CycleCam;
    //public Camera BallroomCam;
    //public Camera HoFCam;
    //public Camera RedCarpetCam;
    //public Camera DotGobblerCam;

    public void GoBallroomCam()
    {
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        BallroomCam.enabled = true;
        //MoveCamera
        BallroomCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoControlRoomCam()
    {
        HitmanGameManager.KillAllCameras();
        //Switch Camera
        ControlRoomCam.enabled = true;
        //Move Camera   
        ControlRoomCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoRedCarpetCam()
    {
        HitmanGameManager.KillAllCameras();
        RedCarpetCam.enabled = true;
        RedCarpetCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoHoFCam()
    {
        HitmanGameManager.KillAllCameras();
        HoFCam.enabled = true;
        HoFCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoCableCam()
    {
        HitmanGameManager.KillAllCameras();
        CableCam.enabled = true;
        CableCam.GetComponent<SplineWalker>().StartWalking();
    }

    public void GoCycleCam()
    {
        HitmanGameManager.KillAllCameras();
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

}
