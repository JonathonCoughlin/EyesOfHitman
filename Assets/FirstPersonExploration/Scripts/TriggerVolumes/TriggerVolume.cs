using UnityEngine;
using System.Collections;


namespace FirstPersonExploration
{
    public enum TriggerVolumeType { Enter, Exit, EnterLimit, ExitLimit, EnterOnCount, ExitOnCount, EveryEnter, EveryExit }

    [RequireComponent(typeof(Collider))]
    public class TriggerVolume : MonoBehaviour
    {

        private Collider m_Collider;

        //TriggerParameters
        public TriggerVolumeType m_triggerType;
        public int m_triggerMagicNumber;

        //TriggerStates
        protected int m_enterCount = 0;
        protected int m_exitCount = 0;
        protected int m_triggerCount = 0;

        // Use this for initialization
        void Start()
        {
            SetComponents();
        }

        protected virtual void SetComponents()
        {
            m_Collider = GetComponent<Collider>();
        }

        void OnTriggerEnter(Collider hitMe)
        {
            if (hitMe.tag == "Player")
            {
                m_enterCount++;
                ManagePlayerEnter();
            }
        }

        protected void ManagePlayerEnter()
        {
            switch (m_triggerType)
            {
                case TriggerVolumeType.Enter:
                    {
                        if (m_enterCount == 1) TriggerMe();
                        break;
                    }
                case TriggerVolumeType.EnterLimit:
                    {
                        if (m_enterCount <= m_triggerMagicNumber) TriggerMe();
                        break;
                    }
                case TriggerVolumeType.EnterOnCount:
                    {
                        if (m_enterCount == m_triggerMagicNumber) TriggerMe();
                        break;
                    }
                case TriggerVolumeType.EveryEnter:
                    {
                        TriggerMe();
                        break;
                    }
            }
        }

        void OnTriggerExit(Collider hitMe)
        {
            if (hitMe.tag == "Player")
            {
                m_exitCount++;
                ManagePlayerExit();
            }
        }

        protected void ManagePlayerExit()
        {
            switch (m_triggerType)
            {
                case TriggerVolumeType.Exit:
                    {
                        if (m_exitCount == 1) TriggerMe();
                        break;
                    }
                case TriggerVolumeType.ExitLimit:
                    {
                        if (m_exitCount <= m_triggerMagicNumber) TriggerMe();
                        break;
                    }
                case TriggerVolumeType.ExitOnCount:
                    {
                        if (m_exitCount == m_triggerMagicNumber) TriggerMe();
                        break;
                    }
                case TriggerVolumeType.EveryExit:
                    {
                        TriggerMe();
                        break;
                    }
            }
        }

        protected virtual void TriggerMe()
        {
            m_triggerCount++;
        }

    }
}