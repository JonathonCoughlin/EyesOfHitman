using UnityEngine;
using System.Collections;

namespace JonClickSystem
{
    //Required components
    [RequireComponent(typeof(ClickEventManager))]
    [RequireComponent(typeof(Collider))]
    public class ClickableObjectComponent : MonoBehaviour
    {

        //Parameters
        public float clickableDistance;
        public float visionConeAngleToAllowClick;
        public bool highlightWhenLookedAt;
        public float highlightScale;
        public int[] colliderLayersToIgnore;

        //States
        private bool playerIsClose;
        private bool amHighlighted;
        private Color originalColor;
        private Color highlightColor;
        private int raycastLayerMask;
        private bool clickCheckingPaused = false;

        //Components
        private GameObject myClickableObject;
        private GameObject thePlayer;
        public bool highlightObjectOverride;
        private GameObject highlightableObject;
        public GameObject overrideObject;
        //Required Components
        private ClickEventManager myClickManager;

        // Use this for initialization
        void Start()
        {
            SetComponents();
            SetLayerMask();
            SetColors();
        }

        private void SetComponents()
        {
            if (highlightObjectOverride)
            {
                highlightableObject = overrideObject;
            } else
            {
                highlightableObject = this.gameObject;
            }
            myClickableObject = this.gameObject;
            thePlayer = GameObject.FindGameObjectWithTag("Player");
            myClickManager = GetComponent<ClickEventManager>();
        }

        private void SetLayerMask()
        {
            //Initialize layermask
            raycastLayerMask = 0;
            //flip bits of layers to ignore
            foreach (int ignoreLayer in colliderLayersToIgnore)
            {
                raycastLayerMask |= (1 << ignoreLayer);
            }
            //invery mask for use with raycast function
            raycastLayerMask = ~raycastLayerMask;
        }

        private void SetColors()
        {
            originalColor = highlightableObject.GetComponent<Renderer>().material.color;
            highlightColor = originalColor;
            highlightColor.g = originalColor.g * highlightScale;
            highlightColor.b = originalColor.b * highlightScale;
            highlightColor.r = originalColor.r * highlightScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (!clickCheckingPaused)
            {
                CheckPlayerDistance();
                if (playerIsClose)
                {
                    //if (!PlayerInMenu() && IsPlayerStaringAtMe())
                    if (IsPlayerStaringAtMe())
                    {
                        if (!amHighlighted)
                        {
                            HighlightMe();
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            PlayerClickedMe();
                        }
                    }
                    else if (amHighlighted)
                    {
                        RemoveHighlight();
                    }
                }
            }
        }

        private void CheckPlayerDistance()
        {
            //is player in range?
            bool playerInRange = Vector3.Distance(myClickableObject.transform.position, thePlayer.transform.position) < clickableDistance;
            //Has PlayerIsClose status changed? Update, if so.
            if (playerInRange != playerIsClose)
            {
                if (playerInRange)
                {
                    RegisterPlayerClose();
                }
                else
                {
                    RegisterPlayerFar();
                }
            }
        }

        //private bool PlayerInMenu()
        //{
        //    return thePlayer.GetComponent<FPEControllerOverrides>().browsingMenu;
        //}

        private bool IsPlayerStaringAtMe()
        {
            bool playerIsStaringAtMe = false;
            Vector3 cameraToObjectVector = (myClickableObject.transform.position - Camera.main.transform.position);
            //check if myClickableObject is within player's cone of vision
            float sightAnglePlayerToMe = Mathf.Abs(Vector3.Angle(cameraToObjectVector, Camera.main.transform.forward));
            //Check if player has clean raycast to myClickableObject (no others in the way)
            if (sightAnglePlayerToMe <= visionConeAngleToAllowClick)
            {
                //Set direction between camera and my object
                Vector3 rayDirection = cameraToObjectVector.normalized;
                //Make the camera ray, perform the raycast
                Ray cameraRay = new Ray(Camera.main.transform.position, rayDirection);
                RaycastHit staringAtWhat;
                Physics.Raycast(cameraRay, out staringAtWhat, Mathf.Infinity, raycastLayerMask);
                //check if the raycast hit ME
                playerIsStaringAtMe = (staringAtWhat.collider.gameObject == myClickableObject.gameObject);
            }
            return playerIsStaringAtMe;
        }

        private void HighlightMe()
        {
            Debug.Log("Tried Highlighting");
            amHighlighted = true;
            highlightableObject.GetComponent<Renderer>().material.color = highlightColor;
        }

        private void RemoveHighlight()
        {
            amHighlighted = false;
            highlightableObject.GetComponent<Renderer>().material.color = originalColor;
        }
        private void PlayerClickedMe()
        {
            //What happens when I'm clicked?
            myClickManager.RegisterClick(this);
        }

        private void RegisterPlayerClose()
        {
            playerIsClose = true;
        }

        private void RegisterPlayerFar()
        {
            playerIsClose = false;
        }

        public void PauseClickFunction()
        {
            clickCheckingPaused = true;
        }

        public void ResumeClickFunction()
        {
            clickCheckingPaused = false;
        }
    }
}