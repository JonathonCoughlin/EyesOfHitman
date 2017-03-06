using UnityEngine;
using System.Collections;

public enum AudioTriggerType { Enter, Exit, EnterTwice }

[RequireComponent(typeof(AudioSource))]
public class AudioTrigger : MonoBehaviour {

    private AudioSource m_Voice;
    public AudioClip m_dialogClip;
    public AudioTriggerType m_triggerType;
    public bool m_loop;

    private int m_enterCount = 0;
    private int m_exitCount = 0;

    public bool m_triggerOnAlternativeObject = false;
    public GameObject m_triggerObject;


    //Speech States
    public bool m_rampAudio;
    private bool m_vocal = false;
    public float m_rampTimeConstant;
    private float m_rampSlope = 0f;
    public float m_resetDelay;
    private float m_timeSinceClipEnd = 0f;
    private float m_maxVolume = 0f;



    // Use this for initialization
    void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_Voice = GetComponent<AudioSource>();
    }
    
	// Update is called once per frame
	void Update () {
        if (m_vocal && m_rampAudio)
        {
            m_Voice.volume += m_rampSlope * Time.deltaTime;
            if (m_Voice.volume <= 0f)
            {
                m_Voice.volume = 0f;
                m_rampSlope = 0f;
                PauseSpeak();
            }
            else if (m_Voice.volume > m_maxVolume)
            {
                m_Voice.volume = m_maxVolume;
                m_rampSlope = 0f;
            }

            if (!m_Voice.isPlaying)
            {
                if (m_timeSinceClipEnd < m_resetDelay)
                {
                    m_timeSinceClipEnd += Time.deltaTime;
                }
                else
                {
                    Speak();
                }
            }
        }
    }

    public void Reset()
    {
        m_Voice.Stop();
        m_enterCount = 0;
        m_exitCount = 0;
    }


    void OnTriggerExit(Collider other)
    {
        bool properGameObject = false;
        if (m_triggerOnAlternativeObject)
        {
            properGameObject = other.gameObject == m_triggerObject;

        } else
        {
            properGameObject = other.gameObject.tag == "Player";
        }        

        if (properGameObject)
        {
            m_exitCount++;
            if (m_exitCount == 1 && m_triggerType == AudioTriggerType.Exit)
            {
                if (m_rampAudio)
                {
                    BeginSpeaking();
                }
                else
                {
                    PlayDialog();
                }
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        bool properGameObject = false;
        if (m_triggerOnAlternativeObject)
        {
            properGameObject = other.gameObject == m_triggerObject;

        }
        else
        {
            properGameObject = other.gameObject.tag == "Player";
        }

        if (properGameObject)
        {
            m_enterCount++;
            switch (m_enterCount)
            {
                case 1:
                    {
                        if (m_triggerType == AudioTriggerType.Enter)
                        {
                            if (m_rampAudio)
                            {
                                BeginSpeaking();
                            } else
                            {
                                PlayDialog();
                            }
                            
                        }
                        break;
                    }
                case 2:
                    {
                        if (m_triggerType == AudioTriggerType.EnterTwice)
                        {
                            if (m_rampAudio)
                            {
                                BeginSpeaking();
                            }
                            else
                            {
                                PlayDialog();
                            }
                        }
                        break;
                    }
            }
        }
    }

    //Speech
    public void SilenceMe()
    {
        m_Voice.volume = 0f;
        m_Voice.Stop();
    }

    public void BeginSpeaking()
    {
        if (!m_vocal)
        {
            Speak();
        }
        RampVolumeUp();
    }

    private void RampVolumeUp()
    {
        m_rampSlope = m_maxVolume / m_rampTimeConstant;
    }

    private void RampVolumeDown()
    {
        m_rampSlope = -m_maxVolume / m_rampTimeConstant;
    }

    public void EndSpeaking()
    {
        RampVolumeDown();
    }

    public void Speak()
    {
        m_Voice.Play();
        m_vocal = true;
        m_timeSinceClipEnd = 0f;
    }

    public void PauseSpeak()
    {
        m_Voice.Pause();
        m_vocal = false;
    }

    private void PlayDialog()
    {
        if (m_loop)
        {
            m_Voice.loop = true;
            m_Voice.clip = m_dialogClip;
            m_Voice.Play();
        } else
        {
            m_Voice.PlayOneShot(m_dialogClip);
        }        
    }

    
}
