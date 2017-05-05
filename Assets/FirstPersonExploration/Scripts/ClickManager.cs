using UnityEngine;
using System.Collections;

namespace FirstPersonExploration
{

    public class ClickManager : MonoBehaviour
    {

        //Parameters
        public LayerMask m_raycastLayerMask;

        //Components
        private GameObject m_Player;
        public Camera m_MainCamera;

        //Overrides
        public bool m_useNonCamObjAsPointer = false;
        public GameObject m_NonCamPointer;

        //States
        public bool m_clicksAllowed { get; private set; }
        private bool m_lookingAtClickableObject = false;
        private ClickableObject m_currentObject;
        private KeyCode m_interactionKey;
        private float m_interactionDistance;

        // Use this for initialization
        void Start()
        {
            SetComponents();
            m_clicksAllowed = true;
        }

        private void SetComponents()
        {
            m_Player = GameObject.FindGameObjectWithTag("Player");
        }

        public void RegisterMainCamera(Camera camera)
        {
            m_MainCamera = camera;
        }

        public void RegisterPlayer(GameObject tempPlayer)
        {
            m_Player = tempPlayer;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_clicksAllowed && CheckLookAndRange())
            {
                CheckClickObject();
            }
        }

        private bool CheckLookAndRange()
        {
            //Raycast
            Ray cameraRay;
            if (m_useNonCamObjAsPointer) {
                cameraRay = m_MainCamera.ScreenPointToRay(m_MainCamera.WorldToScreenPoint(m_NonCamPointer.transform.position));
            }
            else
            {
                cameraRay = new Ray(m_MainCamera.transform.position, m_MainCamera.transform.forward);
            }
                
            RaycastHit staringAtWhat;
            Physics.Raycast(cameraRay, out staringAtWhat, Mathf.Infinity, m_raycastLayerMask);
            //check if the raycast hit a clickable object
            if (StaringAtClickableObject(staringAtWhat))
            {
                ClickableObject tempObject = staringAtWhat.collider.gameObject.GetComponent<ClickableObject>();
                if (CheckPlayerRange(tempObject))
                {
                    SetObject(tempObject);
                }
                else if (m_lookingAtClickableObject)
                {
                    UnsetObject();
                }
                
            } else if (m_lookingAtClickableObject)
            {
                UnsetObject();
            }

            return m_lookingAtClickableObject;
        }

        private void SetObject(ClickableObject tempFocus)
        {
            m_lookingAtClickableObject = true;
            m_currentObject = tempFocus;
            m_interactionKey = m_currentObject.m_interactionKey;
            m_interactionDistance = m_currentObject.m_maxClickDistance;
            m_currentObject.GainFocus();
        }

        private void UnsetObject()
        {
            m_lookingAtClickableObject = false;
            m_currentObject.LoseFocus();
        }

        private bool StaringAtClickableObject(RaycastHit staringAtWhat)
        {
            bool staringAtClickableObject = false;
            if (staringAtWhat.collider != null)
            {
                if (staringAtWhat.collider.gameObject.GetComponent<ClickableObject>() != null)
                {
                    staringAtClickableObject = true;
                }
            }

            return staringAtClickableObject;
        }
        
        private void CheckClickObject()
        {
            if (Input.GetKeyDown(m_interactionKey))
            {
                m_currentObject.RegisterClick();
            }
        }

        private bool CheckPlayerRange(ClickableObject tempObject)
        {
            
            Vector3 playerPosition = m_Player.transform.position;
            Vector3 closestPtOnCollider = tempObject.m_Collider.ClosestPointOnBounds(playerPosition);
            float distanceToPlayer = Vector3.Distance(playerPosition, closestPtOnCollider);
            bool playerInRange = distanceToPlayer < tempObject.m_maxClickDistance;
            
            return playerInRange;
        }

    }

}
