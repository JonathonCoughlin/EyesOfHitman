using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class FlashCamera : MonoBehaviour {

    private Animator m_Animator;

    public enum FlickerType { Flicker1, Flicker2};
    public bool m_flicker = false;
    public FlickerType m_type;

	// Use this for initialization
	void Start () {
        SetComponents();

	}
	
    private void SetComponents()
    {
        m_Animator = GetComponent<Animator>();
        if (m_flicker)
        {
            switch (m_type)
            {
                case FlickerType.Flicker1:
                    {
                        m_Animator.SetBool("Flicker", true);
                        break;
                    }
                case FlickerType.Flicker2:
                    {
                        m_Animator.SetBool("Flicker2", true);
                        break;
                    }
            }
        }
        
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void FlashMe()
    {
        m_Animator.SetTrigger("TakePicture");
    }
}
