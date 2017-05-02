using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class CanonClickEvents : ClickEventManager
    {
        // Components
        public CanonLight m_CanonLight;
        private AudioSource m_buttonClickSounder;

        private bool m_LightOn = false;
        
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
            if (!m_LightOn)
            {
                On();
            } else
            {
                Off();
            }
            
        }

        private void On()
        {
            m_CanonLight.TurnOn();
            m_buttonClickSounder.PlayOneShot(m_buttonClickSounder.clip);
            m_LightOn = true;
            AssassinationCues reportCueMaster = (AssassinationCues)FindObjectOfType(typeof(AssassinationCues));
            reportCueMaster.CannonLightOn();
        }
        
        private void Off()
        {
            m_CanonLight.TurnOff();
            m_buttonClickSounder.PlayOneShot(m_buttonClickSounder.clip);
            m_LightOn = false;
            AssassinationCues reportCueMaster = (AssassinationCues)FindObjectOfType(typeof(AssassinationCues));
            reportCueMaster.CannonLightOff();
        }
    }
}