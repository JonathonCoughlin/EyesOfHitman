using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class HologramClickEvents : ClickEventManager
    {
        // Components
        public HologramClown m_Hologram;
        
        void Start()
        {
            SetComponents();               
        }

        private void SetComponents()
        {
            
        }

        public override void RegisterClick(ClickableObjectComponent wasClicked)
        {
            CountClick();
            ClickMachine();
        }

        private void ClickMachine()
        {
            m_Hologram.ClickMachine();
        }
        
    }
}