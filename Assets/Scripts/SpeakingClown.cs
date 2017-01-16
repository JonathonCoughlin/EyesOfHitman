using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class SpeakingClown : MonoBehaviour {

    public AudioClip m_Story;
    public ClownAnimations m_AnimationStateOverride;

    //Animation States
    private int m_animationID;
    private bool m_speaking;
    private bool m_drinking;
    private bool m_static;

    //Speech States
    private bool m_vocal = false;
    public float m_rampTimeConstant;
    private float m_rampSlope = 0f;
    public float m_resetDelay;
    private float m_timeSinceClipEnd = 0f;

    //Components
    private AudioSource m_Voice;
    private Animator m_Animator;

	// Use this for initialization
	void Start () {
        SetComponents();
	}

    private void SetComponents()
    {
        m_Voice = GetComponent<AudioSource>();
        m_Voice.clip = m_Story;
        m_Animator = GetComponent<Animator>();
        SetAnimationFromKey();
        SilenceMe();
    }
	


    public void SetMyStates(ClownAnimations myAnimation, AudioClip myMonologue, float restartDelay)
    {
        m_AnimationStateOverride = myAnimation;
        m_Story = myMonologue;
        if (m_Voice != null)
        {
            m_Voice.clip = m_Story;
        }
        
        m_resetDelay = restartDelay;
    }

	// Update is called once per frame
	void Update () {
	    if (m_vocal)
        {
            m_Voice.volume += m_rampSlope * Time.deltaTime;
            if (m_Voice.volume <= 0f)
            {
                m_Voice.volume = 0f;
                m_rampSlope = 0f;
                PauseSpeak();
            }
            else if (m_Voice.volume > 1f)
            {
                m_Voice.volume = 1f;
                m_rampSlope = 0f;
            }

            if (!m_Voice.isPlaying)
            {
                if (m_timeSinceClipEnd < m_resetDelay)
                {
                    m_timeSinceClipEnd += Time.deltaTime;
                } else
                {
                    Speak();
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
        if (!m_vocal) {
            Speak();
        }
        RampVolumeUp();
    }

    private void RampVolumeUp()
    {
        m_rampSlope = 1f / m_rampTimeConstant;
    }

    private void RampVolumeDown()
    {
        m_rampSlope = -1f / m_rampTimeConstant;
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



    // Animation Control
    private void SetAnimatorStates()
    {
        m_Animator.SetInteger("AnimationID", m_animationID);
        m_Animator.SetBool("Speaking", m_speaking);
        m_Animator.SetBool("WithDrink", m_drinking);
        m_Animator.SetBool("Static", m_static);
    }

    private void SetAnimationFromKey()
    {
        switch (m_AnimationStateOverride)
        {
            //Static Poses
            case ClownAnimations.MagicianStaticTrick:
                {
                    m_animationID = 0;
                    m_static = true;
                    break;
                }

            case ClownAnimations.MagicianStaticCrouch:
                {
                    m_animationID = 1;
                    m_static = true;
                    break;
                }

            case ClownAnimations.MagicianStaticPerform:
                {
                    m_animationID = 2;
                    m_static = true;
                    break;
                }
            case ClownAnimations.MagicianStaticFolded:
                {
                    m_animationID = 3;
                    m_static = true;
                    break;
                }
            //Speaking Poses
            case ClownAnimations.PowerStanceOration:
                {
                    m_animationID = 0;
                    m_speaking = true;
                    m_static = false;
                    break;
                }
            case ClownAnimations.GentleOration:
                {
                    m_animationID = 1;
                    m_speaking = true;
                    m_static = false;
                    break;
                }
            //Listening Poses
            case ClownAnimations.CrossedArms:
                {
                    m_animationID = 0;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.TabletopStretch:
                {
                    m_animationID = 1;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.HandsOnHips:
                {
                    m_animationID = 2;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.RubbingFace:
                {
                    m_animationID = 3;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.Dance:
                {
                    m_animationID = 4;
                    m_speaking = false;
                    m_drinking = false;
                    m_static = false;
                    break;
                }
            case ClownAnimations.SympatheticDrinking:
                {
                    m_animationID = 0;
                    m_speaking = false;
                    m_drinking = true;
                    m_static = false;
                    break;
                }
            case ClownAnimations.StrongDrinking:
                {
                    m_animationID = 1;
                    m_speaking = false;
                    m_drinking = true;
                    m_static = false;
                    break;
                }

        }
        SetAnimatorStates();
    }
}
