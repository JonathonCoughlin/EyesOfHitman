using UnityEngine;
using System.Collections;

public class BathroomSequence : MonoBehaviour {

    private Light[] m_Lights;
    private AudioSource m_BackgroundMusic;

    public GameObject m_BathroomMirror;

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

    public void LowResourceSequence()
    {
        TurnOffLights();
        TurnOffReflections();
    }

    public void TurnOffReflections()
    {
        m_BathroomMirror.SetActive(false);
    }

    public void TurnOnReflections()
    {
        m_BathroomMirror.SetActive(true);
    }

    public void StartSequence()
    {
        TurnOnLights();
        TurnOnReflections();
        m_BackgroundMusic.Play();
    }

    public void EndSequence()
    {
        TurnOffLights();
        TurnOffReflections();
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
