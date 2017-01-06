using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    public class HoldablePropClickEvents : ClickEventManager
    {

        public Vector3 m_handBonePosOffset;
        public Vector3 m_handBoneEulerOffset;

        public bool m_throwable;
        public float m_throwSpeed;

        private Vector3 m_throwDirection;
        private bool m_flying = false;

        public bool m_held { get; private set; }

        //Components
        private FPClownController player;

        void Start()
        {
            SetComponents();
            m_held = false;              
        }

        void Update()
        {
            if (m_flying) {
                transform.position += m_throwDirection.normalized * m_throwSpeed * Time.deltaTime;
            }
        }

        private void SetComponents()
        {
            player = (FPClownController) FindObjectOfType(typeof(FPClownController));
        }

        public override void RegisterClick(ClickableObjectComponent wasClicked)
        {
            CountClick();
            if (!player.m_holdingProp)
            {
                GiveMeToPlayer();
            }
        }

        private void GiveMeToPlayer()
        {
            
            player.GrabProp(this.gameObject);
        }

        public void GrabMe()
        {
            this.transform.parent = player.handBone.transform;
            transform.localPosition = m_handBonePosOffset;
            transform.localRotation = Quaternion.Euler(m_handBoneEulerOffset.x, m_handBoneEulerOffset.y, m_handBoneEulerOffset.z);
            m_held = true;
        }

        public void ThrowMe(Vector3 throwVector)
        {
            m_throwDirection = throwVector;
            m_flying = true;
            this.transform.parent = null;
        }
        
    }
}