using UnityEngine;
using System.Collections;

namespace FirstPersonExploration
{
    
    [RequireComponent(typeof(AudioSource))]
    public class Button: ClickableObject
    {

        protected AudioSource m_AudioSource;

        //Parameters
        public bool m_playAudioOnClick;
        public AudioClip m_clickAudio;

        protected override void SetComponents()
        {
            base.SetComponents();
            m_AudioSource = GetComponent<AudioSource>();
        }

        public override void RegisterClick()
        {
            base.RegisterClick();
            Debug.Log("ClickingButton");
            if (m_playAudioOnClick)
            {
                m_AudioSource.PlayOneShot(m_clickAudio);
            }
            
        }

    }



}