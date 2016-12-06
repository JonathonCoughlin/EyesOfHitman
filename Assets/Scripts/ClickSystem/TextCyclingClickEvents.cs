using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class TextCyclingClickEvents : ClickEventManager
    {
        // Components
        public ThreeDText m_ThreeDText;

        //  States
        [SerializeField] private int currentTextIdx;

        // Text
        [SerializeField] private List<string> m_textToCycle;
        
        void Start()
        {
            SetComponents();               
        }

        private void SetComponents()
        {
            DisplayText(currentTextIdx);
        }

        public override void RegisterClick(ClickableObjectComponent wasClicked)
        {
            CountClick();
            CycleText();
        }

        private void CycleText()
        {
            currentTextIdx += 1;
            if (currentTextIdx >= m_textToCycle.Count)
            {
                currentTextIdx = 0;
            }
            DisplayText(currentTextIdx);
        }

        private void DisplayText(int displayIdx)
        {
            m_ThreeDText.ChangeText(m_textToCycle[displayIdx]);
        }

    }
}