using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace FirstPersonExploration
{
    public enum VocalQueueStatus { PlayingQueue, PlayingNonessential, Resting, Empty}
    public class VocalQueuer : MonoBehaviour
    {

        //Reference Objects
        public AudioSource m_Voice;

        //Members
        private List<AudioClip> m_vocalQueue = new List<AudioClip>();
        public float m_restLength = 3f;

        //States
        private VocalQueueStatus m_queueStatus;
        private float m_currentRestTime = 0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ManageQueue();
        }

        #region Voice Machine
        private void SpeakQueueLine(AudioClip m_line)
        {
            m_Voice.PlayOneShot(m_line);
            m_queueStatus = VocalQueueStatus.PlayingQueue;
            m_currentRestTime = 0f;
        }

        private void SpeakNonessential(AudioClip m_line)
        {
            m_Voice.PlayOneShot(m_line);
            m_queueStatus = VocalQueueStatus.PlayingNonessential;
        }

        private void AdvanceQueue()
        {
            SpeakQueueLine(m_vocalQueue[0]);
            RemoveQueueFront();
        }

        private void RemoveQueueFront()
        {
            m_vocalQueue.RemoveRange(0, 1);
        }

        private void ManageQueue()
        {
            switch (m_queueStatus)
            {
                case VocalQueueStatus.Empty:
                    {
                        if (m_vocalQueue.Count > 0) AdvanceQueue();
                        break;
                    }
                case VocalQueueStatus.PlayingQueue:
                    {
                        //When done playing
                        if (!m_Voice.isPlaying)
                        {
                            if (m_vocalQueue.Count > 0)
                            {
                                m_queueStatus = VocalQueueStatus.Resting;
                            }
                            else
                            {
                                m_queueStatus = VocalQueueStatus.Empty;
                            }
                        }
                        break;
                    }
                case VocalQueueStatus.PlayingNonessential:
                    {
                        //When done playing
                        if (!m_Voice.isPlaying)
                        {
                            if (m_vocalQueue.Count > 0)
                            {
                                m_queueStatus = VocalQueueStatus.Resting;
                            } else
                            {
                                m_queueStatus = VocalQueueStatus.Empty;
                            }
                        }
                        break;
                    }
                case VocalQueueStatus.Resting:
                    {
                        m_currentRestTime += Time.deltaTime;
                        if (m_currentRestTime >= m_restLength)
                        {
                            AdvanceQueue();
                        }
                        break;
                    }
            }
        }
        #endregion

        #region Interface
        public void PlayNonEssential(AudioClip tempClip)
        {
            switch (m_queueStatus)
            {
                case VocalQueueStatus.Empty:
                    {
                        SpeakNonessential(tempClip);
                        break;
                    }
                case VocalQueueStatus.Resting:
                    {
                        PlayNonEssential(tempClip);
                        break;
                    }
            }
        }

        public void AddAudioToQueue(AudioClip tempClip)
        {
            m_vocalQueue.Add(tempClip);
        }
        #endregion
    }

}
