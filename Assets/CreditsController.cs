using UnityEngine;
using System.Collections;

public class CreditsController : MonoBehaviour {

    public Animator m_TitleAnimator;

    public CreditsAnimator m_47Animator;
    public Animator m_47LineAnimator;

    public CreditsAnimator m_HandlerAnimator;
    public Animator m_HandlerLineAnimator;

    public Animator m_AnglerLineAnimator;

    public Animator m_TableLineAnimator;

    public Animator m_SelofLineAnimator;

    public Animator m_BradfordLineAnimator;
    public Animator m_BradfordDude;

    public AudioClip m_Click;
    public AudioSource m_SoundEffectsPlayer;
    public AudioClip m_Song2;
    public AudioSource m_SongPlayer;

    public Animator m_StevenLineAnimator;

    public Animator m_SpenceLineAnimator;

    public Animator m_EricLineAnimator;

    public Animator m_PortraitLineAnimator;

    public Animator m_PianoLineAnimator;

    public Animator m_FinalThanksLineAnimator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AnimateTitle()
    {
        m_TitleAnimator.SetTrigger("AnimateMe");
    }

    public void Animate47()
    {
        m_47Animator.AnimateMe();
        m_47LineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateHandler()
    {
        m_HandlerAnimator.AnimateMe();
        m_HandlerLineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateAnglers()
    {
        m_AnglerLineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateTable()
    {
        m_TableLineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateSelof()
    {
        m_SelofLineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateBradford()
    {
        m_BradfordLineAnimator.SetTrigger("AnimateMe");
        m_BradfordDude.SetTrigger("AnimateMe");
    }

    public void SwitchSongs()
    {
        m_SongPlayer.clip = m_Song2;
        m_SongPlayer.loop = true;
        m_SongPlayer.volume = 0.9f;
        m_SongPlayer.Play();

        m_SoundEffectsPlayer.PlayOneShot(m_Click);

    }

    public void AnimateSteven()
    {
        m_StevenLineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateSpence()
    {
        m_SpenceLineAnimator.SetTrigger("AnimateMe");
    }

    public void AnimateEric()
    {
        m_EricLineAnimator.SetTrigger("AnimateMe");
    }

    public void PortraitAnimator()
    {
        m_PortraitLineAnimator.SetTrigger("AnimateMe");
    }

    public void PianoAnimator()
    {
        m_PianoLineAnimator.SetTrigger("AnimateMe");
    }

    public void FinalThanksAnimator()
    {
        m_FinalThanksLineAnimator.SetTrigger("AnimateMe");
    }
}
