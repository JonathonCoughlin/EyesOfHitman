using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class FishJumper : MonoBehaviour {

    public SplineWalker Fish1;
    public SplineWalker Fish2;
    public SplineWalker Fish3;

    private Animator m_animator;


	// Use this for initialization
	void Start () {
        SetComponents();
	}
	
    private void SetComponents()
    {
        m_animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void SendFish()
    {
        m_animator.SetTrigger("SendFish");
    }

    public void GoFish1()
    {
        Fish1.StartWalking();
    }

    public void GoFish2()
    {
        Fish2.StartWalking();
    }

    public void GoFish3()
    {
        Fish3.StartWalking();
    }
}
