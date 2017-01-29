using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class PlayButtonClickEvents : ClickEventManager
    {
        // Components
        public SoundBoard m_Soundboard;
        public AudioSource m_Piano;
        private AudioSource m_buttonClickSounder;
        private AssassinationCues m_cueMaster;
        
        void Start()
        {
            SetComponents();               
        }

        private void SetComponents()
        {
            m_buttonClickSounder = GetComponent<AudioSource>();
            m_cueMaster = (AssassinationCues) FindObjectOfType(typeof(AssassinationCues));
        }

        public override void RegisterClick(ClickableObjectComponent wasClicked)
        {
            CountClick();
            PlayPause();
        }

        private void PlayPause()
        {
            if (m_Soundboard.IsPlaying())
            {
                m_Soundboard.PauseSong();
                m_Piano.UnPause();
            } else
            {
                m_Piano.Pause();
                m_Soundboard.PlaySong();
            }
            m_buttonClickSounder.PlayOneShot(m_buttonClickSounder.clip);
            m_cueMaster.PlayButtonPress();
        }
        
    }
}