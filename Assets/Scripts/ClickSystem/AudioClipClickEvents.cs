using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class AudioClipClickEvents : ClickEventManager
    {
        // Components
        private AudioSource m_buttonClickSounder;
        
        void Start()
        {
            SetComponents();               
        }

        private void SetComponents()
        {
            m_buttonClickSounder = GetComponent<AudioSource>();
        }

        public override void RegisterClick(ClickableObjectComponent wasClicked)
        {
            CountClick();
            if (!m_buttonClickSounder.isPlaying)
            {
                m_buttonClickSounder.PlayOneShot(m_buttonClickSounder.clip);
            }
        }
        
    }
}