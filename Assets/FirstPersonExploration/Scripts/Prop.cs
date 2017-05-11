using UnityEngine;
using System.Collections;

namespace FirstPersonExploration
{
    
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class Prop: ClickableObject
    {
        //Components
        protected Rigidbody m_Rigidbody;
        protected AudioSource m_AudioSource;

        //Parameters
        public Vector3 m_handBoneOffset;
        public Vector3 m_handBoneEulerOffset;
        public FPPropAction m_propAction;
        public bool m_playAudioOnAction;
        public AudioClip m_actionAudio;
        public float m_collisionDelaySec = 0f;

        //States
        protected bool m_beingHeld = false;
        protected bool m_flying = false;

        //Throw helpers
        public float m_throwSpeed;
        protected Vector3 m_throwDirection;
        public AudioClip m_collisionSound;

        protected override void SetComponents()
        {
            base.SetComponents();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_AudioSource = GetComponent<AudioSource>();
        }

        public override void RegisterClick()
        {
            if (!m_beingHeld)
            {
                base.RegisterClick();
                Debug.Log("PickingMeUp");
                m_FPExplorer.GrabProp(this);
            }   
        }

        public void DisableMyBehaviors()
        {
            DisableAllBehaviors();
            StopMyPhysics();
        }

        public bool AmHeld()
        {
            return m_beingHeld;
        }

        public void StopMyPhysics()
        {
            m_Rigidbody.isKinematic = true;
            m_Collider.isTrigger = true;
        }

        public void StickMe()
        {
            if (m_flying)
            {
                StopMyPhysics();
                ActivateAllBehaviors();
            }
        }

        public void HoldMe()
        {
            m_beingHeld = true;
            DisableAllBehaviors();
        }

        public void ReleaseMe()
        {
            m_beingHeld = false;
            ActivateAllBehaviors();
        }

        public void ThrowMe(Vector3 throwVector)
        {
            m_Rigidbody.isKinematic = false;
            StartCoroutine(CollisionDelay());
            this.transform.parent = null;
            Vector3 throwImpulse = throwVector.normalized * m_throwSpeed * m_Rigidbody.mass;
            m_Rigidbody.AddForce(throwImpulse, ForceMode.Impulse);
            ReleaseMe();
            m_flying = true;
        }

        IEnumerator CollisionDelay()
        {
            yield return new WaitForSeconds(m_collisionDelaySec);
            m_Collider.isTrigger = false;
        }

        public virtual void EatMe()
        {
            Destroy(this.gameObject);
        }

        public IEnumerator PauseMyCollisions(float waitSeconds)
        {
            m_Collider.isTrigger = true;
            yield return new WaitForSeconds(waitSeconds);
            m_Collider.isTrigger = false;
        }
        
        void OnCollisionEnter(Collision hitDetails)
        {
            GameObject hitMe = hitDetails.gameObject;
            if (hitMe.layer.ToString() != "softObjects")
            {
                PlayCollisionSound();
            }
        }

        protected void PlayCollisionSound()
        {
            m_AudioSource.PlayOneShot(m_collisionSound);
        }
        

    }



}