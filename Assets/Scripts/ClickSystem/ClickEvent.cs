using UnityEngine;
using System.Collections;

namespace JonClickSystem
{
    public class ClickEventManager : MonoBehaviour
    {

        public int myTotalClicks { get; private set; }
        private ClickableObjectComponent myClickMaster;
        
        // Use this for initialization
        void Start()
        {
            myTotalClicks = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        protected void CountClick()
        {
            if (myTotalClicks != null)
            {
                myTotalClicks++;
            }
            else
            {
                myTotalClicks = 1;
            }
        }

        public virtual void RegisterClick()
        {

        }

        public virtual void RegisterClick(ClickableObjectComponent wasClicked)
        {

        }
    }

    public class ClickEvent : MonoBehaviour
    {
        //Member variables
        public int eventIterationsTriggered { get; private set; }
        public bool eventInProgress { get; private set; }


        public virtual void TriggerEvent()
        {


        }

    }
}