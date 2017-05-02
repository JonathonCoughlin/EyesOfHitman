using UnityEngine;
using System.Collections;

namespace FirstPersonExploration
{
    
    [RequireComponent(typeof(Collider))]
    public class ClickableObject : MonoBehaviour
    {

        //Components
        public Collider m_Collider { get; private set; }
        protected FPExplorer m_FPExplorer;
        protected GameObject m_HighlightableObject;

        //States
        protected bool m_clickingAllowed = true; //master set by game
        protected bool m_displayHot = false;
        protected int m_oldLayer;
        //-Highlighting
        public float m_highlightScale = 2f;
        protected bool m_allowHighlighting = true;
        protected bool m_amHighlighted;
        protected Color m_originalColor;
        protected Color m_highlightColor;


        //Click Parameters
        public KeyCode m_interactionKey = KeyCode.Mouse0;
        //-Boundaries
        public float m_maxClickDistance = 10f;

        void Start()
        {
            SetComponents();
            SetColors();
            m_oldLayer = gameObject.layer;
            ActivateAllBehaviors();

        }

        protected virtual void SetComponents()
        {
            m_Collider = GetComponent<Collider>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            m_FPExplorer = player.GetComponent<FPExplorer>();
            m_HighlightableObject = this.gameObject;
        }

        private void SetColors()
        {
            m_originalColor = m_HighlightableObject.GetComponent<Renderer>().material.color;
            m_highlightColor = m_originalColor;
            m_highlightColor.g = m_originalColor.g * m_highlightScale;
            m_highlightColor.b = m_originalColor.b * m_highlightScale;
            m_highlightColor.r = m_originalColor.r * m_highlightScale;
        }

        public virtual void GainFocus()
        {
            if (m_allowHighlighting)
            {
                DisplayHot();
            }
        }

        public virtual void LoseFocus()
        {
            DisplayCold();
        }

        private void DisplayHot()
        {
            if (!m_displayHot)
            {
                m_HighlightableObject.GetComponent<Renderer>().material.color = m_highlightColor;
            }
            m_displayHot = true;
        }

        private void DisplayCold()
        {
            //Disable display options
            if (m_displayHot)
            {
                m_HighlightableObject.GetComponent<Renderer>().material.color = m_originalColor;
            }            
            m_displayHot = false;
        }

        public void AllowClicking()
        {
            m_clickingAllowed = true;
        }

        public void ForbidClicking()
        {
            m_clickingAllowed = false;
        }

        public void ActivateAllBehaviors()
        {
            gameObject.layer = m_oldLayer;
            AllowClicking();
            m_allowHighlighting = true;
        }

        public void DisableAllBehaviors()
        {
            m_oldLayer = gameObject.layer;
            gameObject.layer = 9; //no click layer
            ForbidClicking();
            LoseFocus();
            m_allowHighlighting = false;
        }

        public virtual void RegisterClick()
        {
            Debug.Log("ClickedMe");
        }

    }



}