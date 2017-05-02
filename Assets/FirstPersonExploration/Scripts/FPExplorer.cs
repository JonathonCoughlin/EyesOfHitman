using UnityEngine;
using System.Collections;


namespace FirstPersonExploration
{
    public enum FPPoseType { None, Grab, HoldingProp, ExaminingProp,
        //Prop Actions
        Throw, Eat, AdjustHold};
    public enum FPPropAction { Throw, Eat, Articulate};

    public class FPExplorer : MonoBehaviour
    {
        //FPDrifter
        private FirstPersonDrifter m_Drifter;

        //Animation Objects
        public Animator m_FPAnimator;
        public GameObject m_HandBone;
        public AudioSource m_Voice;
        public VocalQueuer m_VocalQueue;

        //States
        protected bool m_propInHand = false;
        protected bool m_propAwaitingAction = false;
        protected Prop m_currentProp;
        protected bool m_examiningProp = false;

        // Use this for initialization
        void Start()
        {
            SetComponents();
        }

        private void SetComponents()
        {
            m_Drifter = GetComponent<FirstPersonDrifter>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_propInHand && m_propAwaitingAction)
            {
                CheckClick();
            }
        }

        public FPPoseType MapPropActionToPoseType (FPPropAction tempAction)
        {
            FPPoseType mappedPoseType = FPPoseType.None;
            switch (tempAction)
            {
                case FPPropAction.Eat:
                    {
                        mappedPoseType = FPPoseType.Eat;
                        break;
                    }
                case FPPropAction.Throw:
                    {
                        mappedPoseType = FPPoseType.Throw;
                        break;
                    }
                case FPPropAction.Articulate:
                    {
                        mappedPoseType = FPPoseType.AdjustHold;
                        break;
                    }

            }
            return mappedPoseType;
        }

        private void CheckClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                m_examiningProp = true;
                SetPose(FPPoseType.ExaminingProp);
                
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                m_examiningProp = false;
                SetPose(FPPoseType.HoldingProp);
            }
            else if (!m_examiningProp && Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetPose(MapPropActionToPoseType(m_currentProp.m_propAction));
                
            }
        }

        public void PlayActionAudio()
        {
            m_VocalQueue.PlayNonEssential(m_currentProp.m_actionAudio);
        }

        public void ThrowProp()
        {
            Transform camPos = Camera.main.transform;
            Ray throwRay = new Ray(camPos.position, camPos.forward);
            m_currentProp.ThrowMe(throwRay.direction);
            EmptyHands();
        }

        public void EatProp()
        {
            m_currentProp.EatMe();
            EmptyHands();
        }

        private void EmptyHands()
        {
            m_propInHand = false;
            SetPose(FPPoseType.None);
        }

        public void DestroyProp()
        {
            if (m_propInHand)
            {
                GameObject.Destroy(m_currentProp.gameObject);
                EmptyHands();
            }
        }

        public void GrabProp(Prop tempProp)
        {
            if (!m_propInHand)
            {
                m_currentProp = tempProp;
                SetPose(FPPoseType.Grab);
                m_currentProp.HoldMe();
            }
        }

        public void PlaceProp()
        {
            //Set prop kinematic
            m_currentProp.StopMyPhysics();
            //
            m_currentProp.transform.parent = m_HandBone.transform;
            m_currentProp.transform.localPosition = m_currentProp.m_handBoneOffset;
            m_currentProp.transform.localRotation = Quaternion.Euler(m_currentProp.m_handBoneEulerOffset);
            m_propInHand = true;
            m_propAwaitingAction = true;
            SetPose(FPPoseType.HoldingProp);
        }

        public void SetPose(FPPoseType tempPose)
        {
            switch (tempPose) {
                case FPPoseType.None:
                    {
                        m_FPAnimator.SetBool("HoldingObject", false);
                        m_FPAnimator.SetBool("HoldingAxe", false);
                        m_FPAnimator.SetTrigger("KillProp");
                        break;
                    }

                case FPPoseType.Grab:
                    {
                        m_FPAnimator.SetTrigger("Grab");
                        m_Drifter.SwitchControlTypes(WalkControlLimits.NoWalk, LookControlLimits.NoControl);
                        break;
                    }

                case FPPoseType.HoldingProp:
                    {
                        m_Drifter.SwitchControlTypes(WalkControlLimits.FullControl, LookControlLimits.FullControl);
                        m_FPAnimator.SetBool("HoldingObject", true);
                        m_FPAnimator.SetBool("Examine", false);
                        bool axe = m_currentProp.tag == "axe";
                        m_FPAnimator.SetBool("HoldingAxe", axe);
                        break;
                    }
                case FPPoseType.ExaminingProp:
                    {
                        m_FPAnimator.SetBool("Examine",true);
                        break;
                    }
                case FPPoseType.Eat:
                    {
                        m_FPAnimator.SetTrigger("Eat");
                        m_propAwaitingAction = false;
                        break;
                    }
                case FPPoseType.Throw:
                    {
                        m_FPAnimator.SetTrigger("Throw");
                        m_propAwaitingAction = false;
                        break;
                    }
                case FPPoseType.AdjustHold:
                    {
                        m_FPAnimator.SetTrigger("AdjustHold");
                        m_propAwaitingAction = true;
                        break;
                    }
                
            }


        }
    }
}
