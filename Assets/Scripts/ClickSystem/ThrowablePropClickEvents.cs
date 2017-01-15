using UnityEngine;
using System.Collections.Generic;

namespace JonClickSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowablePropClickEvents : HoldablePropClickEvents
    {

        public float m_throwSpeed;
        public bool m_throwable = true;

        private Vector3 m_throwDirection;
        private bool m_flying = false;
        

        void Update()
        {
            if (m_flying) {
                transform.position += m_throwDirection.normalized * m_throwSpeed * Time.deltaTime;
            }
        }
        
        void OnTriggerEnter(Collider hitMe)
        {
            if (m_flying)
            {
                transform.parent = hitMe.gameObject.transform;
                m_flying = false;
            }

        }
        
        public void ThrowMe(Vector3 throwVector)
        {
            m_throwDirection = throwVector;
            m_flying = true;
            this.transform.parent = null;
        }
        
    }
}