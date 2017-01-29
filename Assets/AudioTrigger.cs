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
	
	}

    public void Reset()
    {
        m_Voice.Stop();
        m_enterCount = 0;
        m_exitCount = 0;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_enterCount++;
            switch (m_enterCount)
            {
                case 1:
                    {
                        if (m_triggerType == AudioTriggerType.Enter)
                        {
                            PlayDialog();
                        }
                        break;
                    }
                case 2:
                    {
                        if (m_triggerType == AudioTriggerType.EnterTwice)
                        {
                            PlayDialog();
                        }
                        break;
                    }
            }
        }
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

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_exitCount++;
            if (m_exitCount == 1 && m_triggerType == AudioTriggerType.Exit)
            {
                PlayDialog();
            }
        }
    }
}
