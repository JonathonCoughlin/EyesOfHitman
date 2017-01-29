using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class BackButtonClickEvents : ClickEventManager
    {
        // Components
        public SoundBoard m_Soundboard;
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
            Back();
        }

        private void Back()
        {
            m_Soundboard.PreviousSong();
            m_buttonClickSounder.PlayOneShot(m_buttonClickSounder.clip);
        }
        
    }
}