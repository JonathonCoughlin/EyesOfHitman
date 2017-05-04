using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class SingleFish : MonoBehaviour {

    //Components
    private Animator m_Animator;
    private Rigidbody m_RigidBody;
    private Collider m_Collider;

    public float m_throwSpeed;

    public GameObject m_hook;
    public GameObject m_hand;
    public Vector3 m_positionAsChild;
    public Vector3 m_rotationAsChild;

   
    // Use this for initialization
	void Start () {
        SetComponents();
	}
	
    public void InitializeMe(GameObject hook, GameObject hand)
    {
        m_hook = hook;
        m_hand = hand;
        SetComponents();
    }

    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_RigidBody.isKinematic = true;
        m_Collider.isTrigger = true;
        transform.SetParent(m_hook.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(90f, 90f, 0f);
        m_hook.GetComponent<Animator>().SetTrigger("ReelMeIn");
        Physics.IgnoreLayerCollision(15, 16, true);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void FishermanAction()
    {
        m_Animator.SetTrigger("FishermanAction");
    }

    public void FlopMe()
    {
        m_Animator.SetInteger("PanicType", 0);
        m_Animator.SetBool("Panicking", true);
    }

    public void WobbleMe()
    {
        m_Animator.SetInteger("PanicType", 1);
        m_Animator.SetBool("Panicking", true);
    }

    public void CalmMe()
    {
        m_Animator.SetBool("Panicking", false);
    }

    public void HookMe()
    {

    }
    
    public void SetMyInHand()
    {
        transform.SetParent(m_hand.transform);
        transform.localPosition = m_positionAsChild;
        transform.localRotation = Quaternion.Euler(m_rotationAsChild.x,m_rotationAsChild.y,m_rotationAsChild.z);
    }

    public void ThrowMe()
    {
        //Add Velocity relative to player rotation
        m_RigidBody.isKinematic = false;
        Vector3 throwDirection = Camera.main.transform.forward;
        m_RigidBody.AddForce(throwDirection.normalized * m_throwSpeed, ForceMode.VelocityChange);
        Destroy(this.gameObject, 5f);
        transform.SetParent(null);
        
    }

    public void AllowCollisions()
    {
        m_Collider.isTrigger = false;
    }
    
}
