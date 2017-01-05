using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FirstPersonDrifter))]
public class FPClownController : MonoBehaviour {

    private FirstPersonDrifter m_Drifter;
    public Animator m_Animator;

    //Prop Holding Stuff
    public bool holdingProp { get; private set; }
    public GameObject handBone;

	// Use this for initialization
	void Start () {
        SetComponents();
        holdingProp = false;
	}
	
    private void SetComponents()
    {
        m_Drifter = GetComponent<FirstPersonDrifter>();
    }

	// Update is called once per frame
	void Update () {
        ManageWalkAnimation();
        if (holdingProp) ManageProp();
	}

    private void ManageWalkAnimation()
    {
        m_Animator.SetBool("Walking", m_Drifter.walking);
    }

    private void ManageProp()
    {

    }

    public void GrabProp(GameObject prop)
    {
        holdingProp = true;
    }

    private void ThrowProp()
    {

    }



}
