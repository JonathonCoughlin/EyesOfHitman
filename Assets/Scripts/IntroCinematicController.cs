using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroCinematicController : MonoBehaviour {

    private Animator m_CinematicAnimator;
    private SplineWalker m_SplineWalker;
    private AudioSource m_AudioSource;

     

	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_CinematicAnimator = GetComponent<Animator>();
        m_SplineWalker = GetComponent<SplineWalker>();
        m_AudioSource = GetComponent<AudioSource>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void BeginIntroCinematic()
    {
        m_CinematicAnimator.SetTrigger("BeginCinematic");
    }

    public void ActivateSoundtrack()
    {
        m_AudioSource.Play();
    }

    public void StartCamera()
    {
        m_SplineWalker.StartWalking();
    }

    public void LoadScene2()
    {
        Debug.Log("Scene Loading");
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }
}
